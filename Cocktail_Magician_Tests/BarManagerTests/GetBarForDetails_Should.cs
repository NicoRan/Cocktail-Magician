using Cocktail_Magician_DB;
using Cocktail_Magician_DB.Models;
using Cocktail_Magician_Services;
using Cocktail_Magician_Services.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Cocktail_Magician_Tests.BarManagerTests
{
    [TestClass]
    public class GetBarForDetails_Should
    {
        private Bar barForTest;
        private User user;
        private Cocktail cocktail;
        private BarReview barReview;
        private BarCocktail barCocktail;
        private Mock<ICocktailManager> cocktailManagerMoq = new Mock<ICocktailManager>();
        private Mock<IBarFactory> barFactoryMoq = new Mock<IBarFactory>();
        public GetBarForDetails_Should()
        {
            barForTest = new Bar()
            {
                BarId = "One",
                Address = "Solunska 2",
                Information = "Information",
                MapDirections = "Go south",
                Name = "Bar",
                Picture = "Picture"
            };

            user = new User()
            {
                Id = "One",
                UserName = "Pesho"
            };

            cocktail = new Cocktail()
            {
                Id = "One",
                Name = "Cocktail"
            };

            barCocktail = new BarCocktail()
            {
                BarId = barForTest.BarId,
                Bar = barForTest,
                CocktailId = cocktail.Id,
                Cocktail = cocktail
            };

            barReview = new BarReview()
            {
                BarId = barForTest.BarId,
                Bar = barForTest,
                Comment = "Comment",
                CreatedOn = DateTime.MinValue,
                Grade = 5d,
                UserId = user.Id,
                User = user
            };
        }
        [TestMethod]
        public async Task ReturnCorrectBar_WithAttached_CorrectCollections()
        {
            var options = TestUtilities.GetOptions(nameof(ReturnCorrectBar_WithAttached_CorrectCollections));

            using (var arrangeContext = new CMContext(options))
            {
                arrangeContext.Bars.Add(barForTest);
                arrangeContext.Cocktails.Add(cocktail);
                arrangeContext.Users.Add(user);
                arrangeContext.BarCocktails.Add(barCocktail);
                arrangeContext.BarReviews.Add(barReview);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CMContext(options))
            {
                var sut = new BarManager(assertContext, cocktailManagerMoq.Object, barFactoryMoq.Object);
                string cocktailName = "";
                string userName = "";
                string comment = "";
                double grade = 0d;
                var bar = await sut.GetBarForDetails(barForTest.BarId);
                foreach (var item in bar.BarCocktailDTOs)
                {
                    cocktailName = item.CocktailName;
                    break;
                }
                foreach (var item in bar.BarReviewDTOs)
                {
                    userName = item.UserName;
                    comment = item.Comment;
                    grade = item.Grade;
                }
                Assert.AreEqual(1, bar.BarCocktailDTOs.Count());
                Assert.AreEqual(cocktail.Name, cocktailName);
                Assert.AreEqual(user.UserName, userName);
                Assert.AreEqual(barReview.Comment, comment);
                Assert.AreEqual(barReview.Grade, grade);
            }
        }

        [TestMethod]
        public async Task ThrowException_WhenId_DoesNotExists()
        {
            var options = TestUtilities.GetOptions(nameof(ThrowException_WhenId_DoesNotExists));

            using (var arrangeContext = new CMContext(options))
            {
                arrangeContext.Bars.Add(barForTest);
                arrangeContext.Cocktails.Add(cocktail);
                arrangeContext.Users.Add(user);
                arrangeContext.BarCocktails.Add(barCocktail);
                arrangeContext.BarReviews.Add(barReview);
                await arrangeContext.SaveChangesAsync();
            }

            using(var assertContext = new CMContext(options))
            {
                var sut = new BarManager(assertContext, cocktailManagerMoq.Object, barFactoryMoq.Object);

                await Assert.ThrowsExceptionAsync<Exception>(() => sut.GetBarForDetails("Three"));
            }
        }

        [TestMethod]
        public async Task ThrowException_WhenIdExists_ButDeletedIsTrue()
        {
            var options = TestUtilities.GetOptions(nameof(ThrowException_WhenIdExists_ButDeletedIsTrue));

            using (var arrangeContext = new CMContext(options))
            {
                arrangeContext.Bars.Add(barForTest);
                arrangeContext.Cocktails.Add(cocktail);
                arrangeContext.Users.Add(user);
                arrangeContext.BarCocktails.Add(barCocktail);
                arrangeContext.BarReviews.Add(barReview);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CMContext(options))
            {
                var sut = new BarManager(assertContext, cocktailManagerMoq.Object, barFactoryMoq.Object);

                await Assert.ThrowsExceptionAsync<Exception>(() => sut.GetBarForDetails("Two"));
            }
        }

        [TestMethod]
        public async Task ShowCorrectMessage_WhenThrowsException()
        {
            var options = TestUtilities.GetOptions(nameof(ShowCorrectMessage_WhenThrowsException));

            var message = "This bar does not exists!";

            using (var arrangeContext = new CMContext(options))
            {
                arrangeContext.Bars.Add(new Bar
                {
                    BarId = "Five",
                    Name = "Gosho"
                });
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CMContext(options))
            {
                var sut = new BarManager(assertContext, cocktailManagerMoq.Object, barFactoryMoq.Object);

                var ex = await Assert.ThrowsExceptionAsync<Exception>(() => sut.GetBarForDetails("Three"));

                Assert.AreEqual(message, ex.Message);
            }
        }
    }
}
