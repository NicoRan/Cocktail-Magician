using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Cocktail_Magician_DB;
using Cocktail_Magician_DB.Models;
using Cocktail_Magician_Services;
using Cocktail_Magician_Services.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Cocktail_Magician_Tests.CocktailManagerTest
{
    [TestClass]
    public class GetAllReviewsByCocktailID_Should
    {
        [TestMethod]
        public async Task GetAllReviewsByCocktailIDId()
        {
            var options = TestUtilities.GetOptions(nameof(GetAllReviewsByCocktailIDId));
            var mockIngredient = new Mock<IIngredientManager>();
            var mockFactory = new Mock<ICocktailFactory>();



            var cocktail = new Cocktail()
            {
                Id = "1",
                Information = "Information",
                Name = "JustAName",
                IsDeleted = false,
                Picture = "SomePicture",
                Rating = 5
            };
            var user = new User()
            {
                Id = "1",
                UserName = "Username"
            };

            var review = new CocktailReview()
            {
                CocktailId = "1",
                Cocktail = cocktail,
                Comment = "someComment",
                CreatedOn = DateTime.Now,
                Grade = 5,
                User = user,
                UserId = "1"
            };

            using (var actContext = new CMContext(options))
            {
                await actContext.Cocktails.AddAsync(cocktail);
                await actContext.Users.AddAsync(user);
                await actContext.SaveChangesAsync();
            }

            using (var assertContext = new CMContext(options))
            {
                var sut = new CocktailManager(mockIngredient.Object, assertContext,mockFactory.Object);
                var result = await sut.GetAllReviewsByCocktailID(cocktail.Id);
                Assert.AreEqual(1,result.Count);
            }
        }
    }
}
