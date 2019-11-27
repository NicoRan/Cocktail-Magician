using Cocktail_Magician_DB;
using Cocktail_Magician_DB.Models;
using Cocktail_Magician_Services;
using Cocktail_Magician_Services.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace Cocktail_Magician_Tests.BarManagerTests
{
    [TestClass]
    public class GetBar_Should
    {
        private Bar barForTest;
        private Bar bar2;
        private Mock<ICocktailManager> cocktailManagerMoq = new Mock<ICocktailManager>();
        private Mock<IBarFactory> barFactoryMoq = new Mock<IBarFactory>();
        public GetBar_Should()
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

            bar2 = new Bar()
            {
                BarId = "Two",
                Address = "Borisova",
                Information = "Information",
                MapDirections = "Go south",
                Name = "Bar2",
                Picture = "Picture",
                IsDeleted = true
            };
        }

        [TestMethod]
        public async Task Return_TheCorrectBar_ByBarId()
        {
            var options = TestUtilities.GetOptions(nameof(Return_TheCorrectBar_ByBarId));

            using (var arrangeContext = new CMContext(options))
            {
                arrangeContext.Bars.Add(barForTest);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CMContext(options))
            {
                var sut = new BarManager(assertContext, cocktailManagerMoq.Object, barFactoryMoq.Object);
                var bar = await sut.GetBar(barForTest.BarId);

                Assert.AreEqual(bar.Address, barForTest.Address);
                Assert.AreEqual(bar.Name, barForTest.Name);
                Assert.AreEqual(bar.Information, barForTest.Information);
                Assert.AreEqual(bar.MapDirection, barForTest.MapDirections);
                Assert.AreEqual(bar.Picture, barForTest.Picture);
            }
        }

        [TestMethod]
        public async Task ThrowExeption_WhenId_DoesNotExists()
        {
            var options = TestUtilities.GetOptions(nameof(ThrowExeption_WhenId_DoesNotExists));

            using (var arrangeContext = new CMContext(options))
            {
                arrangeContext.Bars.Add(barForTest);
                arrangeContext.Bars.Add(bar2);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CMContext(options))
            {
                var sut = new BarManager(assertContext, cocktailManagerMoq.Object, barFactoryMoq.Object);

                await Assert.ThrowsExceptionAsync<Exception>(() => sut.GetBar("Three"));
            }
        }

        [TestMethod]
        public async Task ThrowExeption_WhenId_ExistsButDeletedIsTrue()
        {
            var options = TestUtilities.GetOptions(nameof(ThrowExeption_WhenId_ExistsButDeletedIsTrue));

            using (var arrangeContext = new CMContext(options))
            {
                arrangeContext.Bars.Add(barForTest);
                arrangeContext.Bars.Add(bar2);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CMContext(options))
            {
                var sut = new BarManager(assertContext, cocktailManagerMoq.Object, barFactoryMoq.Object);

                await Assert.ThrowsExceptionAsync<Exception>(() => sut.GetBar("Two"));
            }
        }

        [TestMethod]
        public async Task ShowCorrectMessage_WhenThrowsException()
        {
            var options = TestUtilities.GetOptions(nameof(ShowCorrectMessage_WhenThrowsException));

            var message = "This bar does not exists!";

            using (var arrangeContext = new CMContext(options))
            {
                arrangeContext.Bars.Add(barForTest);
                arrangeContext.Bars.Add(bar2);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CMContext(options))
            {
                var sut = new BarManager(assertContext, cocktailManagerMoq.Object, barFactoryMoq.Object);

                var ex = await Assert.ThrowsExceptionAsync<Exception>(() => sut.GetBar("Two"));

                Assert.AreEqual(message, ex.Message);
            }
        }
    }
}
