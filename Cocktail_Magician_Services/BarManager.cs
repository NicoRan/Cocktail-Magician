using Cocktail_Magician_DB;
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

        public BarManager(CMContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task CreateBar(BarDTO barToCreate)
        {
            var barToAdd = barToCreate.ToBar();

            var barToFind = _context.Bars.SingleOrDefault(bar => bar.Name == barToAdd.Name && bar.Address == barToAdd.Address && !bar.IsDeleted);

            if (barToFind != null)
            {
                throw new InvalidOperationException("Bar already exists in the database");
            }

            await _context.Bars.AddAsync(barToAdd);
            await _context.SaveChangesAsync();
        }

        //public async Task EditBar(Bar bar, List<string> cocktailsToOffer)
        //{
        //    foreach (var cocktail in cocktailsToOffer)
        //    {
        //        bar.BarCocktails.Add(new BarCocktailDTO)
        //    }
        //}

        public async Task RemoveBar(string id)
        {
            var barToRemove = await GetBar(id);
            barToRemove.IsDeleted = true;
            _context.Bars.Update(barToRemove.ToBar());
            await _context.SaveChangesAsync();
        }

        public async Task<BarDTO> GetBar(string id)
        {
            try
            {
                var bar = await _context.Bars
                    .Include(b => b.BarCocktails)
                        .ThenInclude(b => b.Cocktail)
                    .Include(b => b.BarReviews)
                        .ThenInclude(br => br.User)
                    .Where(b => !b.IsDeleted)
                    .FirstOrDefaultAsync(b => b.BarId == id);

                return bar.ToDTO();
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

        //UNECESSARY REFACTORED METHOD
        public async Task<ICollection<BarDTO>> GetTopRatedBars()
        {
            //Why, Nikki, calculate the rating when in database you have a property holding the average rating of the bar and why not just update it in some other method???
            
            var allBars = await GetAllBarsAsync();
            
            var bars = allBars
                 .OrderByDescending(bar =>
                    bar.BarReviewDTOs.Any(br => br.BarId == bar.Id) ? bar.BarReviewDTOs.Average(br => br.Grade) : 0)
                 .ThenBy(bar => bar.Name);
            //Why the above is faster then this:
            //var topRatedv = await _context.Bars.OrderByDescending(tr => tr.Rating).ThenBy(b => b.Name).Take(6).ToListAsync();
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

        public async Task<ICollection<BarDTO>> GetAllBarsAsync()
        {
            var listOfBars = await _context.Bars
                .Include(b => b.BarCocktails)
                    .ThenInclude(b => b.Cocktail)
                .Include(c => c.BarReviews)
                    .ThenInclude(br => br.User)
                .Where(b => !b.IsDeleted)
                .ToListAsync();
            return listOfBars.ToDTO();
        }

        public async Task<ICollection<BarReviewDTO>> GetAllReviewsByBarID(string barId)
        {
            var reviews = await _context.BarReviews.Where(br => br.BarId == barId).ToListAsync();

            return reviews.ToDTO();
        }
    }
}