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

        public async Task RemoveBar(string id)
        {
            var bars = await GetCocktailEntity(id);
            
            bars.IsDeleted = true;
            _context.Bars.Update(bars);
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

        public async Task<ICollection<BarDTO>> GetTopRatedBars()
        {
            var topRatedBars = _context.Bars
                .Include(br => br.BarReviews)
                    .ThenInclude(br=>br.User)
                .Include(bc => bc.BarCocktails)
                    .ThenInclude(c => c.Cocktail)
                .OrderByDescending(b => b.Rating)
                .ThenBy(b => b.Name)
                .Where(b=>b.IsDeleted == false)
                .Take(6)
                .Select(b => b.ToDTO());
            
            return await topRatedBars.ToListAsync();
        }

        public async Task<BarReviewDTO> CreateBarReviewAsync(BarReviewDTO barReviewDTO)
        {
            if (barReviewDTO.Grade != 0)
            {
                var barReview = barReviewDTO.ToEntity();

                await _context.BarReviews.AddAsync(barReview);
                await _context.SaveChangesAsync();

                await UpdateRating(barReviewDTO.BarId);
            }
            else
                await _context.SaveChangesAsync();

            return barReviewDTO;
        }

        private async Task UpdateRating(string barId)
        {
            var rating = _context.BarReviews
                .Where(b => b.BarId == barId)
                .Average(br => br.Grade);
            
            var bar = _context.Bars.Find(barId);
            bar.Rating = rating;
            _context.Bars.Update(bar);

            await _context.SaveChangesAsync();
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

        private async Task<Bar> GetCocktailEntity(string barId)
        {
            return await _context.Bars.FirstOrDefaultAsync(c => c.BarId == barId);
        }
    }
}