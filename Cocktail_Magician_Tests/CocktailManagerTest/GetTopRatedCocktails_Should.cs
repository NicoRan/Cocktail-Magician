using Cocktail_Magician_DB;
using Cocktail_Magician_DB.Models;
using Cocktail_Magician_Services;
using Cocktail_Magician_Services.Contracts;
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
    public class GetTopRatedCocktails_Should
    {
        [TestMethod]
        public async Task ReturnCorrentCount()
        {
            var options = TestUtilities.GetOptions(nameof(ReturnCorrentCount));
            var mockIngredient = new Mock<IIngredientManager>();
            var mockFactory = new Mock<ICocktailFactory>();


            var cocktailOne = new Cocktail()
            {
                Name = "name",
                Information = "info",
                Picture = "picture",
                Rating = 4.7
            };
            var cocktailTwo = new Cocktail()
            {
                Name = "name",
                Information = "info",
                Picture = "picture",
                Rating = 4.6

            };
            var cocktailThree = new Cocktail()
            {
                Name = "name",
                Information = "info",
                Picture = "picture",
                Rating = 4.5
            };
            var cocktailFour = new Cocktail()
            {
                Name = "name",
                Information = "info",
                Picture = "picture",
                Rating = 4.4
            };
            var cocktailFive = new Cocktail()
            {
                Name = "name",
                Information = "info",
                Picture = "picture",
                Rating = 4.1
            };

            using( var arrangeContext = new CMContext(options))
            {
                await arrangeContext.AddAsync(cocktailOne);
                await arrangeContext.AddAsync(cocktailTwo);
                await arrangeContext.AddAsync(cocktailThree);
                await arrangeContext.AddAsync(cocktailFour);
                await arrangeContext.AddAsync(cocktailFive);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CMContext(options))
            {
                var sut = new CocktailManager(mockIngredient.Object, assertContext,mockFactory.Object);
                var result = await sut.GetTopRatedCocktails();
                Assert.AreEqual(4,result.Count());
            }

        }
        [TestMethod]
        public async Task ReturntOrderedResult()
        {
            var options = TestUtilities.GetOptions(nameof(ReturntOrderedResult));
            var mockIngredient = new Mock<IIngredientManager>();
            var mockFactory = new Mock<ICocktailFactory>();


            var cocktailOne = new Cocktail()
            {
                Name = "name",
                Information = "info",
                Picture = "picture",
                Rating = 4.7
            };
            var cocktailTwo = new Cocktail()
            {
                Name = "name",
                Information = "info",
                Picture = "picture",
                Rating = 4.6

            };
            var cocktailThree = new Cocktail()
            {
                Name = "name",
                Information = "info",
                Picture = "picture",
                Rating = 4.5
            };
            var cocktailFour = new Cocktail()
            {
                Name = "name",
                Information = "info",
                Picture = "picture",
                Rating = 4.4
            };
            var cocktailFive = new Cocktail()
            {
                Name = "name",
                Information = "info",
                Picture = "picture",
                Rating = 4.1
            };

            using( var arrangeContext = new CMContext(options))
            {
                await arrangeContext.AddAsync(cocktailOne);
                await arrangeContext.AddAsync(cocktailTwo);
                await arrangeContext.AddAsync(cocktailThree);
                await arrangeContext.AddAsync(cocktailFour);
                await arrangeContext.AddAsync(cocktailFive);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CMContext(options))
            {
                var sut = new CocktailManager(mockIngredient.Object, assertContext,mockFactory.Object);
                var result = await sut.GetTopRatedCocktails();
                Assert.AreEqual(cocktailOne.Rating,result.ElementAt(0).Rating);
                Assert.AreEqual(cocktailTwo.Rating,result.ElementAt(1).Rating);
                Assert.AreEqual(cocktailThree.Rating,result.ElementAt(2).Rating);
                Assert.AreEqual(cocktailFour.Rating,result.ElementAt(3).Rating);
            }

        }
    }
}
