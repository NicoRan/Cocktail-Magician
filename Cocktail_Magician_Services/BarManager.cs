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
        private readonly IBarFactory _barFactory;

        public BarManager(CMContext context, ICocktailManager cocktailManager, IBarFactory barFactory)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _cocktailManager = cocktailManager ?? throw new ArgumentNullException(nameof(cocktailManager));
            _barFactory = barFactory ?? throw new ArgumentNullException(nameof(barFactory));
        }

        /// <summary>
        /// This method adds the new Bar to the DataBase after checking if it does not exists already
        /// </summary>
        /// <param name="barToCreate">This is the newly created Bar object</param>
        /// <returns>Task</returns>
        public async Task CreateBar(BarDTO barToCreate)
        {
            try
            {
                var barToAdd = _barFactory.CreateNewBar(barToCreate.Name, barToCreate.Address, barToCreate.Information, barToCreate.Picture, barToCreate.MapDirection);

                var barToFind = _context.Bars.SingleOrDefault(bar => bar.Name == barToAdd.Name && bar.Address == barToAdd.Address && !bar.IsDeleted);

                if (barToFind != null)
                {
                    throw new InvalidOperationException("Bar already exists in the database!");
                }
                await _context.Bars.AddAsync(barToAdd);
                await _context.SaveChangesAsync();
            }
            catch (InvalidOperationException)
            {
                throw new InvalidOperationException("Bar already exists in the database!");
            }
            catch (Exception)
            {
                throw new Exception("Wrong parameters for Bar!");
            }

        }

        /// <summary>
        /// This method Adds the new Review to the Database and calls the private method UpdateRating
        /// </summary>
        /// <param name="barReviewDTO">BarReviewDTO object with data for the review (UserId, 
        /// BarId, Rating, Text, DateCreated the review</param>
        /// <returns>BarReviewDTO</returns>
        public async Task<BarReviewDTO> CreateBarReviewAsync(BarReviewDTO barReviewDTO)
        {
            if (barReviewDTO.Grade > 0)
            {
                var barReview = _barFactory.CreateNewBarReview(barReviewDTO.Grade, barReviewDTO.Comment, barReviewDTO.UserId, barReviewDTO.BarId, barReviewDTO.DateCreated);

                await _context.BarReviews.AddAsync(barReview);
                await _context.SaveChangesAsync();

                await UpdateRating(barReviewDTO.BarId);
            }
            else
                throw new InvalidOperationException("Cannot comment a Bar without giving it a rating!");

            return barReviewDTO;
        }

        /// <summary>
        /// This method receives a bar object, Lists of string cocktail names to add and another list with names
        /// to remove. Takes from the databes the bar object with the same id, modifieds the properties with the new
        /// ones, and  modifieds the collection of BarCocktails after checking if the lists with names are full.
        /// </summary>
        /// <param name="bar">The Bar object with modified properties</param>
        /// <param name="cocktailsToOffer">List of string names of new Cocktails to Offer</param>
        /// <param name="cocktailsToRemove">List of string names of Cocktails to remove</param>
        /// <returns>Task</returns>
        public async Task EditBar(BarDTO bar, ICollection<string> cocktailsToOffer, ICollection<string> cocktailsToRemove)
        {
            var barToUpdate = await _context.Bars.Include(b => b.BarCocktails).FirstOrDefaultAsync(b => b.BarId == bar.Id);
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

        /// <summary>
        /// This method returns all the Bars from the Database which property IsDeleted = false, with no
        /// attached Collections
        /// </summary>
        /// <returns>List of BarDTO</returns>
        public async Task<ICollection<BarDTO>> GetAllBarsAsync()
        {
            var listOfBars = await _context.Bars
                .OrderByDescending(b => b.Rating)
                .Where(b => !b.IsDeleted)
                .ToListAsync();
            if (listOfBars.Count() == 0 || listOfBars == null)
                throw new NullReferenceException("No bars were found!");
            return listOfBars.ToDTO();
        }

        /// <summary>
        /// This method finds and checks if IsDeleted = true in Database and returns the Bar with no attached
        /// Collection. It is used for Delete Bar where you need only to modifed prperty IsDeleted
        /// </summary>
        /// <param name="id">Id of the Bar</param>
        /// <returns>BarDTO</returns>
        public async Task<BarDTO> GetBar(string id)
        {
            var bar = await _context.Bars
                .AsNoTracking()
                .Where(b => !b.IsDeleted)
                .FirstOrDefaultAsync(b => b.BarId == id) ?? throw new Exception("This bar does not exists!");

            return bar.ToDTO();
        }

        /// <summary>
        /// This method finds and return the Bar, after checking if property IsDeleted = false, from the Database 
        /// with all of his Collections to display
        /// </summary>
        /// <param name="id">Id of the bar</param>
        /// <returns>BarDTO</returns>
        public async Task<BarDTO> GetBarForDetails(string id)
        {
            var bar = await _context.Bars
                .AsNoTracking()
                .Include(b => b.BarCocktails)
                    .ThenInclude(b => b.Cocktail)
                .Include(b => b.BarReviews)
                    .ThenInclude(br => br.User)
                .Where(b => !b.IsDeleted)
                .FirstOrDefaultAsync(b => b.BarId == id) ?? throw new Exception("This bar does not exists!");
            return bar.ToDTO();
        }

        /// <summary>
        /// This method finds and returns the Bar, after checking if property IsDeleted = false, from the Database 
        /// with only attached the BarCocktail Collection to be modified also
        /// </summary>
        /// <param name="id">Id of the Bar</param>
        /// <returns>BarDTO</returns>
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

        /// <summary>
        /// This method returns the first 6 Bars from the Database sorted by the highest rating
        /// and by Name (if Bars with the exact same rating).
        /// </summary>
        /// <returns>List of BarDTO</returns>
        public async Task<ICollection<BarDTO>> GetTopRatedBars()
        {
            return await _context.Bars
                .OrderByDescending(b => b.Rating)
                .ThenBy(b => b.Name)
                .Where(b => b.IsDeleted == false)
                .Take(6)
                .Select(b => b.ToDTO()).ToListAsync();
        }

        /// <summary>
        /// This method checks if the User with the given Id has already reviewed the Bar 
        /// with the given Id.
        /// </summary>
        /// <param name="barId">Id of the Bar</param>
        /// <param name="userId">Id of the User</param>
        /// <returns>Boolean</returns>
        public async Task<bool> IsReviewGiven(string barId, string userId)
        {
            return await _context.BarReviews.AnyAsync(br => br.BarId == barId && br.UserId == userId);
        }

        /// <summary>
        /// This method takes id and search for the respected Bar.
        /// If found modifieds the prperty IsDeleted to true and updates the Database
        /// </summary>
        /// <param name="id">Id of the Bar to be deleted</param>
        /// <returns>Task</returns>
        public async Task RemoveBar(string id)
        {
            var bars = await GetBar(id);

            bars.IsDeleted = true;
            _context.Bars.Update(bars.ToBar());
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// This method finds all reviews of the Bar and calculates its average rating. After that calls method
        /// GetBarEntity to update the Bars Rating property in the Database.
        /// </summary>
        /// <param name="barId">Id of the Bar</param>
        /// <returns>Task</returns>
        private async Task UpdateRating(string barId)
        {
            var rating = _context.BarReviews
                .Where(b => b.BarId == barId)
                .Average(br => br.Grade);

            var bar = await GetBar(barId);
            bar.Rating = Math.Round(rating, 1);
            _context.Bars.Update(bar.ToBar());

            await _context.SaveChangesAsync();
        }
    }
}