using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cocktail_Magician_DB;
using Cocktail_Magician_DB.Models;
using Cocktail_Magician_Services;
using Cocktail_Magician_Services.Contracts;
using Cocktail_Magician_Services.DTO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Cocktail_Magician_Tests.CocktailManagerTest
{
    [TestClass]
    public class CreateCocktailReview_Should
    {
        [TestMethod]
        public async Task CreateCocktailReview()
        {
            var options = TestUtilities.GetOptions(nameof(CreateCocktailReview));

            var mockIngredient = new Mock<IIngredientManager>();
            var mockFactory = new Mock<ICocktailFactory>();

            var mockCocktailReviewDTO = new CocktailReviewDTO()
            {
                CocktailId = "1",
                Comment = "comment",
                CreatedOn = DateTime.Now,
                Grade = 4,
                UserId = "1",
                UserName = "Nikola",
                UserPicture = "UserPicture"
            };

            var cocktail = new Cocktail()
            { Id = "1", Information = "infomation", Rating = 1 };
            using (var arrangeContext = new CMContext(options))
            {
                await arrangeContext.Cocktails.AddAsync(cocktail);
                await arrangeContext.SaveChangesAsync();
                
            }

            using (var assertContext = new CMContext(options))
            {
                var sut = new CocktailManager(mockIngredient.Object, assertContext, factoryMock.Object);
                await sut.CreateCocktailReviewAsync(mockCocktailReviewDTO);
                Assert.AreEqual(1,assertContext.CocktailReviews.Count());
            }
        }
    }
}
