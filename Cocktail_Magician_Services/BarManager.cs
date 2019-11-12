using Cocktail_Magician_DB;
using Cocktail_Magician_DB.Models;
using Cocktail_Magician_Services.Contracts;
using Cocktail_Magician_Services.DTO;
using Cocktail_Magician_Services.Mappers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cocktail_Magician_Services
{
    public class BarManager : IBarManager
    {
        private readonly CMContext _context;
        private readonly ICocktailManager _cocktailManager;

        public BarManager(CMContext context, ICocktailManager cocktailManager)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _cocktailManager = cocktailManager ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Bar> CreateBar(Bar barToCreate)
        {
            Bar barToAdd;
            try
            {
                barToAdd = barToCreate;
            }
            catch
            {
                throw new ArgumentException("Wrong parameters are passed!");
            }

            var barToFind = _context.Bars.SingleOrDefault(bar => bar.Name == barToAdd.Name && bar.Address == barToAdd.Address);

            if (barToFind != null)
            {
                throw new InvalidOperationException("Bar already exists in the catalogue");
            }

            await _context.Bars.AddAsync(barToAdd);
            await _context.SaveChangesAsync();
            return barToAdd;
        }

        public async Task RemoveBar(string id)
        {
            Bar barToRemove = await GetBar(id);
            barToRemove.IsDeleted = true;
            _context.Bars.Update(barToRemove);
            await _context.SaveChangesAsync();
        }

        public async Task<Bar> GetBar(string id)
        {
            var bar = await _context.Bars.Where(b => !b.IsDeleted).FirstOrDefaultAsync(b => b.BarId == id);
            bar.Cocktails = await GetBarsOfferedCocktails(bar.BarId);
            return bar;
        }

        public async Task<List<Bar>> GetTopRatedBars()
        {
            var bars = await _context.Bars
                .OrderByDescending(bar => bar.Rating)
                .ThenBy(bar => bar.Name)
                .Take(6)
                .ToListAsync();
            return bars;
        }

        public async Task<BarReviewDTO> CreateBarReviewAsync(BarReviewDTO barReviewDTO)
        {
            if (barReviewDTO.Grade != 0)
            {
                var barRating = barReviewDTO.ToRatingEntity();

                await _context.BarRatings.AddAsync(barRating);
                await _context.SaveChangesAsync();

            }

            if (barReviewDTO.Comment != null)
            {
                var barComment = barReviewDTO.ToCommentEntity();

                await _context.BarComments.AddAsync(barComment);
                await _context.SaveChangesAsync();
            }

            return barReviewDTO;
        }

        public async Task<List<Cocktail>> GetBarsOfferedCocktails(string barId)
        {
            var listOfCocktails = new List<Cocktail>();
            var result = await _context.BarCocktails.ToListAsync();
            foreach (var item in result)
            {
                if (item.BarId == barId)
                {
                    var cocktail = await _cocktailManager.GetCocktail(item.CocktailId);
                    listOfCocktails.Add(cocktail);
                }
            }
            return listOfCocktails;
        }
    }
}