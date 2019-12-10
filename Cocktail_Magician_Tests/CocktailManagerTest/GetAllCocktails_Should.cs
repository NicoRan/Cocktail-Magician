using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Cocktail_Magician_DB;
using Cocktail_Magician_DB.Models;
using Cocktail_Magician_Services;
using Cocktail_Magician_Services.Contracts;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Cocktail_Magician_Tests.CocktailManagerTest
{
    [TestClass]
    public class GetAllCocktails_Should
    {
        [TestMethod]
        public async Task GetAllCocktails()
        {
            var options = TestUtilities.GetOptions(nameof(GetAllCocktails));
            var mockIngredient = new Mock<IIngredientManager>();
            var mockFactory = new Mock<ICocktailFactory>();

            var cocktail = new Cocktail()
            {
                Id = "1",
                Information = "informtion",
                IsDeleted = false,
                Name = "Name",
                Picture = "Picture",
                Rating = 4
            };

            using (var actContext = new CMContext(options))
            {
                await actContext.Cocktails.AddAsync(cocktail);
                await actContext.SaveChangesAsync();
                
            }

            using (var assertContext = new CMContext(options))
            {
                var sut = new CocktailManager(mockIngredient.Object, assertContext, mockFactory.Object);
                var result = await sut.GetAllCocktailsAsync();
                Assert.AreEqual(1,result.Count);
            }
        }
    }
}
