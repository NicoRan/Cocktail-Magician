﻿using Cocktail_Magician_DB;
using Cocktail_Magician_DB.Models;
using Cocktail_Magician_Services.Contracts;
using Cocktail_Magician_Services.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cocktail_Magician_Services.Mappers;

namespace Cocktail_Magician_Services
{
    public class CocktailManager : ICocktailManager
    {
        private readonly CMContext _context;
        private readonly IIngredientManager _ingredientManager;

        public CocktailManager(IIngredientManager ingredientManager, CMContext context)
        {
            _context = context;
            _ingredientManager = ingredientManager;
        }

        public async Task CreateCocktail(CocktailDTO cocktail, List<string> ingredients)
        {
            var cocktailToFind = _context.Cocktails.SingleOrDefault(c => c.Name == cocktail.Name);
            if (cocktailToFind != null)
            {
                throw new InvalidOperationException("Cocktail already exists in the database");
            }

            var cocktailToAdd = cocktail.ToEntity();
            cocktailToAdd.CocktailIngredient = new List<CocktailIngredient>();
            foreach (var ingredient in ingredients)
            {
                var findIngredient = await _ingredientManager.FindIngredientByNameAsync(ingredient);
                if (findIngredient != null)
                {
                    cocktailToAdd.CocktailIngredient.Add(new CocktailIngredient() { Cocktail = cocktailToAdd, Ingredient = findIngredient });
                }
            }
            cocktailToAdd.Information = CocktailsRecipe(ingredients);
            await _context.Cocktails.AddAsync(cocktailToAdd);
            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<CocktailDTO>> GetTopRatedCocktails()
        {
            var allCocktails = await GetAllCocktailsAsync();

            var cocktails = allCocktails
                .OrderByDescending(c => c.CocktailReviewDTOs.Any(ci => ci.CocktailId == c.Id) ? c.CocktailReviewDTOs.Average(ci => ci.Grade) : 0)
                .ThenBy(cocktail => cocktail.Name);

            return cocktails.Take(4).ToList();
        }

        public async Task<CocktailReviewDTO> CreateCocktailReviewAsync(CocktailReviewDTO cocktailReviewDTO)
        {
            if (cocktailReviewDTO.Grade != 0)
            {
                var cocktailReview = cocktailReviewDTO.ToEntity();

                await _context.CocktailReviews.AddAsync(cocktailReview);
            }

            await _context.SaveChangesAsync();

            return cocktailReviewDTO;
        }

        public async Task<CocktailDTO> GetCocktail(string id)
        {
            try
            {
                var cocktail = await _context.Cocktails
                    .Include(c => c.CocktailIngredient)
                    .Include(c => c.CocktailReviews)
                        .ThenInclude(c=>c.User)
                    .Where(c => !c.IsDeleted)
                    .FirstOrDefaultAsync(c => c.Id == id);
                return cocktail.ToDTO();
            }
            catch (InvalidOperationException)
            {
                throw new Exception();
            }
        }

        public async Task<CocktailDTO> GetCocktailByName(string name)
        {
            try
            {
                var result = await _context.Cocktails.FirstOrDefaultAsync(cocktail => cocktail.Name == name);
                return result.ToDTO();
            }
            catch (Exception)
            {
                throw new Exception("Cocktail not foun!");
            }
        }

        public async Task RemoveCocktail(string id)
        {
            try
            {
                var cocktailToRemove = await GetCocktail(id);
                cocktailToRemove.IsDeleted = true;
                _context.Cocktails.Update(cocktailToRemove.ToEntity());
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception("Cocktail to remove not found!");
            }
        }

        public async Task<bool> IsReviewGiven(string cocktailId, string userId)
        {
            return await _context.CocktailReviews.AnyAsync(cr => cr.CocktailId == cocktailId && cr.UserId == userId);
        }

        public async Task<ICollection<CocktailDTO>> GetAllCocktailsAsync()
        {
            var listCocktails = await _context.Cocktails
                .Include(cocktail => cocktail.CocktailIngredient)
                .Include(c => c.CocktailReviews)
                    .ThenInclude(c => c.User)
                .Where(cocktail => !cocktail.IsDeleted)
                .ToListAsync();
            return listCocktails.ToDTO();
        }

        public async Task<ICollection<CocktailReviewDTO>> GetAllReviewsByCocktailID(string cocktailId)
        {
            var reviews = await _context.CocktailReviews.Include(c => c.User).Where(c => c.CocktailId == cocktailId).ToListAsync();

            return reviews.ToDTO();
        }

        private string CocktailsRecipe(List<string> ingredients)
        {
            var result = String.Join(", ", ingredients.ToArray());
            return result;
        }
    }
}
