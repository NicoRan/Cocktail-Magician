using Cocktail_Magician_DB;
using Cocktail_Magician_DB.Models;
using Cocktail_Magician_Services;
using Cocktail_Magician_Services.Contracts;
using Cocktail_Magician_Services.DTO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cocktail_Magician_Tests.CocktailManagerTest
{
    [TestClass]
    public class CreateCocktail_Should
    {
        [TestMethod]
        public async Task CreateCocktail()
        {
            var options = TestUtilities.GetOptions(nameof(CreateCocktail));
            var mockIngredient = new Mock<IIngredientManager>();
            var igredients = new List<string> { "first" };
            var ingredient = new Ingredient()
            {
                Name = "first",
            };

            var cocktailDTO = new CocktailDTO()
            {
                Name = "cocktailName",
                Information = "cocktail infromation",
                Picture = "picture",
            };


            using (var arrangeContext = new CMContext(options))
            {
                await arrangeContext.Ingredients.AddAsync(ingredient);
                await arrangeContext.SaveChangesAsync();
                var sut = new CocktailManager(mockIngredient.Object, arrangeContext);
                await sut.CreateCocktail(cocktailDTO, igredients);

            }
            using (var assertContext = new CMContext(options))
            {
                Assert.AreEqual(1, assertContext.Cocktails.Count());
            }
        }
        [TestMethod]
        public async Task ThrowExceptionIfCocktailNameExist()
        {
            var options = TestUtilities.GetOptions(nameof(ThrowExceptionIfCocktailNameExist));
            var mockIngredient = new Mock<IIngredientManager>();
            var igredients = new List<string> { "first" };
            var ingredient = new Ingredient()
            {
                Name = "first",
            };

            var cocktailDTO = new CocktailDTO()
            {
                Name = "cocktailName",
                Information = "cocktail infromation",
                Picture = "picture"
            };

            var cocktail = new Cocktail()
            {
                Name = "cocktailName",
                Information = "cocktail infromation",
                Picture = "picture"
            };
            using (var arrangeContext = new CMContext(options))
            {
                await arrangeContext.Ingredients.AddAsync(ingredient);
                await arrangeContext.Cocktails.AddAsync(cocktail);
                await arrangeContext.SaveChangesAsync();

            }
            using (var assertContext = new CMContext(options))
            {
                var sut = new CocktailManager(mockIngredient.Object, assertContext);
                await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => sut.CreateCocktail(cocktailDTO, igredients));
            }
        }        
        [TestMethod]
        public async Task ThrowCorrentMessageExcpetion()
        {
            var options = TestUtilities.GetOptions(nameof(ThrowExceptionIfCocktailNameExist));
            var mockIngredient = new Mock<IIngredientManager>();
            var igredients = new List<string> { "first" };
            var ingredient = new Ingredient()
            {
                Name = "first",
            };

            var cocktailDTO = new CocktailDTO()
            {
                Name = "cocktailName",
                Information = "cocktail infromation",
                Picture = "picture"
            };

            var cocktail = new Cocktail()
            {
                Name = "cocktailName",
                Information = "cocktail infromation",
                Picture = "picture"
            };
            using (var arrangeContext = new CMContext(options))
            {
                await arrangeContext.Ingredients.AddAsync(ingredient);
                await arrangeContext.Cocktails.AddAsync(cocktail);
                await arrangeContext.SaveChangesAsync();

            }
            using (var assertContext = new CMContext(options))
            {
                var sut = new CocktailManager(mockIngredient.Object, assertContext);
                await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => sut.CreateCocktail(cocktailDTO, igredients), "Cocktail already exists in the");
            }
        }
    }
}
