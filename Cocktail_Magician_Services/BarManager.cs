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
            _cocktailManager = cocktailManager ?? throw new ArgumentNullException(nameof(cocktailManager));
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

        public async Task EditBar(BarDTO bar, ICollection<string> cocktailsToOffer, ICollection<string> cocktailsToRemove)
        {
            var barToUpdate = _context.Bars.Include(b => b.BarCocktails).Include(b => b.BarReviews).FirstOrDefault(b => b.BarId == bar.Id);
            barToUpdate.Address = bar.Address;
            barToUpdate.Information = bar.Information;
            barToUpdate.MapDirections = bar.MapDirection;
            barToUpdate.Name = bar.Name;
            barToUpdate.Picture = bar.Picture;
            if (cocktailsToOffer.Count() > 0 && cocktailsToRemove.Count() > 0)
            {
                foreach (var cocktail in cocktailsToRemove)
                {
                    var cocktailToRemove = await _cocktailManager.GetCocktailByName(cocktail);
                    _context.BarCocktails.Remove(barToUpdate.BarCocktails.FirstOrDefault(bc => bc.BarId == barToUpdate.BarId && bc.CocktailId == cocktailToRemove.Id));
                }
                foreach (var cocktail in cocktailsToOffer)
                {
                    var cocktailToAdd = await _cocktailManager.GetCocktailByName(cocktail);
                    barToUpdate.BarCocktails.Add(new BarCocktail()
                    {
                        Bar = barToUpdate,
                        Cocktail = cocktailToAdd.ToEntity()
                    });
                }
            }
            else if (cocktailsToOffer.Count() > 0 && cocktailsToRemove.Count() == 0)
            {
                foreach (var cocktail in cocktailsToOffer)
                {
                    var cocktailToAdd = await _cocktailManager.GetCocktailByName(cocktail);
                    barToUpdate.BarCocktails.Add(new BarCocktail()
                    {
                        Bar = barToUpdate,
                        Cocktail = cocktailToAdd.ToEntity()
                    });
                }
            }
            else if (cocktailsToOffer.Count() == 0 && cocktailsToRemove.Count() > 0)
            {
                foreach (var cocktail in cocktailsToRemove)
                {
                    var cocktailToRemove = await _cocktailManager.GetCocktailByName(cocktail);

                    _context.BarCocktails.Remove(barToUpdate.BarCocktails.FirstOrDefault(bc => bc.BarId == barToUpdate.BarId && bc.CocktailId == cocktailToRemove.Id));
                }
            }

            _context.Bars.Update(barToUpdate);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveBar(string id)
        {
            var bars = await GetBarEntity(id);
            
            bars.IsDeleted = true;
            _context.Bars.Update(bars);
            await _context.SaveChangesAsync();
        }

        public async Task<BarDTO> GetBarForDetails(string id)
        {
            try
            {
                var bar = await _context.Bars
                    .AsNoTracking()
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

        public async Task<BarDTO> GetBarForEditAsync(string id)
        {
            try
            {
                var bar = await _context.Bars
                    .AsNoTracking()
                    .Include(b => b.BarCocktails)
                        .ThenInclude(b => b.Cocktail)
                    .Where(b => !b.IsDeleted)
                    .FirstOrDefaultAsync(b => b.BarId == id);

                return bar.ToDTO();
            }
            catch (Exception)
            {
                throw new Exception("This bar does not exists!");
            }
        }

        public async Task<BarDTO> GetBar(string id)
        {
            try
            {
                var bar = await _context.Bars
                    .AsNoTracking()
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
            return await _context.Bars
                .OrderByDescending(b => b.Rating)
                .ThenBy(b => b.Name)
                .Where(b => b.IsDeleted == false)
                .Take(6)
                .Select(b => b.ToDTO()).ToListAsync();
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

        public async Task<ICollection<BarDTO>> GetAllBarsAsync()
        {
            var listOfBars = await _context.Bars
                .Where(b => !b.IsDeleted)
                .ToListAsync();
            return listOfBars.ToDTO();
        }

        public async Task<ICollection<BarReviewDTO>> GetAllReviewsByBarID(string barId)
        {
            var reviews = await _context.BarReviews.Where(br => br.BarId == barId).ToListAsync();

            return reviews.ToDTO();
        }

        public async Task<ICollection<BarCocktailDTO>> GetAllBarCocktailsByBarId(string id)
        {
            var barCocktails = await _context.BarCocktails.AsNoTracking().Include(bc => bc.Cocktail).Where(bc => bc.BarId == id).ToListAsync();
            return barCocktails.ToDTO();
        }

        private async Task UpdateRating(string barId)
        {
            var rating = _context.BarReviews
                .Where(b => b.BarId == barId)
                .Average(br => br.Grade);

            var bar = await GetBarEntity(barId);
            bar.Rating = Math.Round(rating, 1);
            _context.Bars.Update(bar);

            await _context.SaveChangesAsync();
        }

        private async Task<Bar> GetBarEntity(string barId)
        {
            return await _context.Bars.FirstOrDefaultAsync(c => c.BarId == barId);
        }
    }
}