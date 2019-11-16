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
            var barToAdd = barToCreate;

            var barToFind = _context.Bars.SingleOrDefault(bar => bar.Name == barToAdd.Name && bar.Address == barToAdd.Address && !bar.IsDeleted);

            if (barToFind != null)
            {
                throw new InvalidOperationException("Bar already exists in the database");
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
            try
            {
                var bar = await _context.Bars.Where(b => !b.IsDeleted).FirstOrDefaultAsync(b => b.BarId == id);
                bar.Cocktails = await GetBarsOfferedCocktails(bar.BarId);
                return bar;
            }
            catch (Exception)
            {
                throw new Exception("This bar does not exists!");
            }
        }

        public async Task<List<Bar>> GetTopRatedBars()
        {
            var bars = await _context.Bars
                .Where(bar => !bar.IsDeleted)
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
            var result = await _context.BarCocktails.Where(r => r.BarId == barId && !r.Bar.IsDeleted).ToListAsync();
            foreach (var item in result)
            {
                var cocktail = await _cocktailManager.GetCocktail(item.CocktailId);
                listOfCocktails.Add(cocktail);
            }
            return listOfCocktails;
        }

        public async Task<List<Bar>> GetAllBarsAsync()
        {
            var listOfBars = await _context.Bars.Include(b => b.Cocktails).Where(b => !b.IsDeleted).ToListAsync();
            return listOfBars;
        }
    }
}