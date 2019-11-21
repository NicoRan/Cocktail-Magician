using Cocktail_Magician_DB;
using Cocktail_Magician_DB.Models;
using Cocktail_Magician_Services.Contracts;
using Cocktail_Magician_Services.DTO;
using Cocktail_Magician_Services.Mappers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
                var bar = await _context.Bars
                    .Include(b => b.BarReviews)
                        .ThenInclude(br => br.User)
                    .Where(b => !b.IsDeleted)
                    .FirstOrDefaultAsync(b => b.BarId == id);
                bar.Cocktails = await GetBarsOfferedCocktails(bar.BarId);
                return bar;
            }
            catch (Exception)
            {
                throw new Exception("This bar does not exists!");
            }
        }

        public async Task<bool> IsReviewGiven(string barId, string userId)
        {
            return await _context.BarReviews.AnyAsync(br => br.BarId == barId && br.UserId == userId);
        }

        public async Task<List<Bar>> GetTopRatedBars()
        {
            var allBars = await GetAllBarsAsync();

            var bars = allBars
                 .OrderByDescending(bar =>
                    bar.BarReviews.Any(br => br.BarId == bar.BarId) ?       bar.BarReviews.Average(br => br.Grade) : 0)
                 .ThenBy(bar => bar.Name);

            return bars.Take(6).ToList();
        }

        public async Task<BarReviewDTO> CreateBarReviewAsync(BarReviewDTO barReviewDTO)
        {
            if (barReviewDTO.Grade != 0)
            {
                var barReview = barReviewDTO.ToEntity();

                await _context.BarReviews.AddAsync(barReview);
                await _context.SaveChangesAsync();

            }
            await _context.SaveChangesAsync();

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
            var listOfBars = await _context.Bars
                .Include(b => b.Cocktails)
                .Include(c => c.BarReviews)
                    .ThenInclude(br => br.User)
                .Where(b => !b.IsDeleted)
                .ToListAsync();
            return listOfBars;
        }

        public async Task<ICollection<BarReviewDTO>> GetAllReviewsByBarID(string barId)
        {
            var reviews = await _context.BarReviews.Where(br => br.BarId == barId).ToListAsync();

            return reviews.ToDTO();
        }

    }
}