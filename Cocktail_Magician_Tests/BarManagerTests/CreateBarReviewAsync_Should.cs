using Cocktail_Magician_DB;
using Cocktail_Magician_DB.Models;
using Cocktail_Magician_Services;
using Cocktail_Magician_Services.Contracts;
using Cocktail_Magician_Services.Mappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Cocktail_Magician_Tests.BarManagerTests
{
    [TestClass]
    public class CreateBarReviewAsync_Should
    {
        private Bar barForTest;
        private User userForTests;
        private BarReview createReviewForTest;
        private Mock<ICocktailManager> cocktailManagerMoq = new Mock<ICocktailManager>();
        private Mock<IBarFactory> barFactoryMoq = new Mock<IBarFactory>();
        public CreateBarReviewAsync_Should()
        {
            barForTest = new Bar()
            {
                BarId = "One",
                Address = "Solunska 2",
                Information = "Information",
                MapDirections = "Go south",
                Name = "Bar",
                Picture = "Picture",
                Rating = 4.5d
            };

            userForTests = new User()
            {
                Id = "TheOne",
                UserName = "UserName",
                Picture = "Picture"
            };

            createReviewForTest = new BarReview()
            {
                Bar = barForTest,
                BarId = barForTest.BarId,
                Comment = "Comment",
                CreatedOn = DateTime.MinValue,
                Grade = 3.5d,
                User = userForTests,
                UserId = userForTests.Id
            };
        }

        [TestMethod]
        public async Task AddReview_When_ValidValuesArePassed()
        {
            var options = TestUtilities.GetOptions(nameof(AddReview_When_ValidValuesArePassed));

            using (var arrangeContext = new CMContext(options))
            {
                arrangeContext.Bars.Add(barForTest);
                arrangeContext.Users.Add(userForTests);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CMContext(options))
            {
                var sut = new BarManager(assertContext, cocktailManagerMoq.Object, barFactoryMoq.Object);
                await sut.CreateBarReviewAsync(createReviewForTest.ToDTO());

                Assert.AreEqual(1, assertContext.BarReviews.Count());
            }
        }

        [TestMethod]
        public async Task ThrowInvalidOperationException_When_NoRatingIsGiven()
        {
            var options = TestUtilities.GetOptions(nameof(ThrowInvalidOperationException_When_NoRatingIsGiven));

            var barReviewTest = new BarReview()
            {
                Bar = barForTest,
                BarId = barForTest.BarId,
                Comment = "Message",
                CreatedOn = DateTime.MinValue,
                Grade = 0d,
                User = userForTests,
                UserId = userForTests.Id
            };

            using (var arrangeContext = new CMContext(options))
            {
                arrangeContext.Bars.Add(barForTest);
                arrangeContext.Users.Add(userForTests);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CMContext(options))
            {
                var sut = new BarManager(assertContext, cocktailManagerMoq.Object, barFactoryMoq.Object);

                await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => sut.CreateBarReviewAsync(barReviewTest.ToDTO()));
            }
        }

        [TestMethod]
        public async Task ShowCorrectMessage_When_ThrowsInvalidOperationException()
        {
            var options = TestUtilities.GetOptions(nameof(ShowCorrectMessage_When_ThrowsInvalidOperationException));

            var message = "Cannot comment a Bar without giving it a rating!";
            var barReviewTest = new BarReview()
            {
                Bar = barForTest,
                BarId = barForTest.BarId,
                Comment = "Message",
                CreatedOn = DateTime.MinValue,
                Grade = 0d,
                User = userForTests,
                UserId = userForTests.Id
            };

            using (var arrangeContext = new CMContext(options))
            {
                arrangeContext.Bars.Add(barForTest);
                arrangeContext.Users.Add(userForTests);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CMContext(options))
            {
                var sut = new BarManager(assertContext, cocktailManagerMoq.Object, barFactoryMoq.Object);

                var ex = await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => sut.CreateBarReviewAsync(barReviewTest.ToDTO()));

                Assert.IsTrue(ex.Message.Contains(message));
            }
        }
    }
}
