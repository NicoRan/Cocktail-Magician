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
    public class GetAllBarsAsync_Should
    {
        private Bar bar1;
        private Bar bar2;
        private Bar bar3;
        private Mock<ICocktailManager> cocktailManagerMoq = new Mock<ICocktailManager>();
        private Mock<IBarFactory> barFactoryMoq = new Mock<IBarFactory>();
        public GetAllBarsAsync_Should()
        {
            bar1 = new Bar()
            {
                Address = "Solunska 1",
                Information = "Information",
                MapDirections = "Go south",
                Name = "Bar1",
                Picture = "Picture"
            };

            bar2 = new Bar()
            {
                Address = "Solunska 2",
                Information = "Information",
                MapDirections = "Go south",
                Name = "Bar2",
                Picture = "Picture"
            };

            bar3 = new Bar()
            {
                Address = "Solunska 2",
                Information = "Information",
                MapDirections = "Go south",
                Name = "Bar2",
                Picture = "Picture",
                IsDeleted = true
            };
        }

        [TestMethod]
        public async Task ReturnAList_OfBarDTO_WhenDatabaseNotEmpty()
        {
            var options = TestUtilities.GetOptions(nameof(ReturnAList_OfBarDTO_WhenDatabaseNotEmpty));

            using (var arrangeContext = new CMContext(options))
            {
                arrangeContext.Bars.Add(bar1);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CMContext(options))
            {
                var sut = new BarManager(assertContext, cocktailManagerMoq.Object, barFactoryMoq.Object);
                var result = await sut.GetAllBarsAsync();

                Assert.AreEqual(1, result.Count());
            }
        }

        [TestMethod]
        public async Task ReturnList_OfBarDTO_WithOnlyBarsIsDeletedFalse()
        {
            var options = TestUtilities.GetOptions(nameof(ReturnList_OfBarDTO_WithOnlyBarsIsDeletedFalse));

            using (var arrangeContext = new CMContext(options))
            {
                arrangeContext.Bars.Add(bar1);
                arrangeContext.Bars.Add(bar2);
                arrangeContext.Bars.Add(bar3);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CMContext(options))
            {
                var sut = new BarManager(assertContext, cocktailManagerMoq.Object, barFactoryMoq.Object);
                var result = await sut.GetAllBarsAsync();

                Assert.AreEqual(2, result.Count());
            }
        }

        [TestMethod]
        public async Task ThrowNullException_WhenList_IsEmpty()
        {
            var options = TestUtilities.GetOptions(nameof(ThrowNullException_WhenList_IsEmpty));

            using (var assertContext = new CMContext(options))
            {
                var sut = new BarManager(assertContext, cocktailManagerMoq.Object, barFactoryMoq.Object);

                await Assert.ThrowsExceptionAsync<NullReferenceException>(() => sut.GetAllBarsAsync());
            }
        }

        [TestMethod]
        public async Task ShowCorrectMessage_WhenList_IsEmpty()
        {
            var options = TestUtilities.GetOptions(nameof(ShowCorrectMessage_WhenList_IsEmpty));

            var message = "No bars were found!";

            using (var assertContext = new CMContext(options))
            {
                var sut = new BarManager(assertContext, cocktailManagerMoq.Object, barFactoryMoq.Object);

                var ex = await Assert.ThrowsExceptionAsync<NullReferenceException>(() => sut.GetAllBarsAsync());

                Assert.AreEqual(message, ex.Message);
            }
        }
    }
}
