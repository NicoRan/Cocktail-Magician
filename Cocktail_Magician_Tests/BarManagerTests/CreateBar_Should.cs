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
    public class CreateBar_Should
    {
        private Bar barForTest;
        private Mock<ICocktailManager> cocktailManagerMoq = new Mock<ICocktailManager>();
        private Mock<IBarFactory> barFactoryMoq = new Mock<IBarFactory>();
        public CreateBar_Should()
        {
            barForTest = new Bar()
            {
                Address = "Solunska 2",
                Information = "Information",
                MapDirections = "Go south",
                Name = "Bar",
                Picture = "Picture",
                Rating = 4.5d
            };
        }
        [TestMethod]
        public async Task AddBar_ToDatabase_WhenValidValuesPassed()
        {
            var options = TestUtilities.GetOptions(nameof(AddBar_ToDatabase_WhenValidValuesPassed));
            var barTest = new Bar()
            {
                Address = "Borisov",
                Information = "Information",
                MapDirections = "North",
                Name = "Bar",
                Picture = "Picture",
                Rating = 4.5d
            };

            barFactoryMoq.Setup(bf => bf.CreateNewBar(barTest.Name, barTest.Address, barTest.Information, barTest.Picture, barTest.MapDirections)).Returns(barTest);

            using (var assertContext = new CMContext(options))
            {
                var sut = new BarManager(assertContext, cocktailManagerMoq.Object, barFactoryMoq.Object);
                await sut.CreateBar(barTest.ToDTO());

                Assert.AreEqual(1, assertContext.Bars.Count());
            }
        }

        [TestMethod]
        public async Task Calls_BarFactory_CreateNewBar_AtLeastOnce_WhenValidValuesArePassed()
        {
            var options = TestUtilities.GetOptions(nameof(Calls_BarFactory_CreateNewBar_AtLeastOnce_WhenValidValuesArePassed));
            var barTest = new Bar()
            {
                Address = "Borisov 3",
                Information = "Information",
                MapDirections = "North",
                Name = "Kobrat",
                Picture = "Picture",
                Rating = 4.5d
            };

            barFactoryMoq.Setup(bf => bf.CreateNewBar(barTest.Name, barTest.Address, barTest.Information, barTest.Picture, barTest.MapDirections)).Returns(barTest);

            using (var assertContext = new CMContext(options))
            {
                var sut = new BarManager(assertContext, cocktailManagerMoq.Object, barFactoryMoq.Object);
                await sut.CreateBar(barTest.ToDTO());

                barFactoryMoq.Verify(bf => bf.CreateNewBar(barTest.Name, barTest.Address, barTest.Information, barTest.Picture, barTest.MapDirections), Times.Once);
            }
        }

        [TestMethod]
        public async Task ThrowsException_WhenBar_AlreadyInDatabase()
        {
            var options = TestUtilities.GetOptions(nameof(ThrowsException_WhenBar_AlreadyInDatabase));

            using (var arrangeContext = new CMContext(options))
            {
                arrangeContext.Bars.Add(barForTest);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CMContext(options))
            {
                var sut = new BarManager(assertContext, cocktailManagerMoq.Object, barFactoryMoq.Object);
                await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => sut.CreateBar(barForTest.ToDTO()));
            }
        }

        [TestMethod]
        public async Task ShowsCorrectMessage_WhenThrowsException()
        {
            var options = TestUtilities.GetOptions(nameof(ShowsCorrectMessage_WhenThrowsException));
            var message = "Bar already exists in the database!";

            using (var arrangeContext = new CMContext(options))
            {
                arrangeContext.Bars.Add(barForTest);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CMContext(options))
            {
                var sut = new BarManager(assertContext, cocktailManagerMoq.Object, barFactoryMoq.Object);
                var ex = await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => sut.CreateBar(barForTest.ToDTO()));

                Assert.AreEqual(message, ex.Message);
            }
        }
    }
}
