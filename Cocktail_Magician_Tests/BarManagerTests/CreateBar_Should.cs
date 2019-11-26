using Cocktail_Magician_DB;
using Cocktail_Magician_DB.Models;
using Cocktail_Magician_Services;
using Cocktail_Magician_Services.Contracts;
using Cocktail_Magician_Services.DTO;
using Cocktail_Magician_Services.Mappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cocktail_Magician_Tests.BarManagerTests
{
    [TestClass]
    public class CreateBar_Should
    {
        [TestMethod]
        public async Task AddBar_ToDatabase_WhenValidValuesPassed()
        {
            var options = TestUtilities.GetOptions(nameof(AddBar_ToDatabase_WhenValidValuesPassed));
            var barForTest = new Bar()
            {
                Address = "Solunska 2",
                Information = "Information",
                MapDirections = "Go south",
                Name = "Bar",
                Picture = "Picture",
                Rating = 4.5d
            };
            var cocktailManagerMoq = new Mock<ICocktailManager>();

            using(var assertContext = new CMContext(options))
            {
                var sut = new BarManager(assertContext, cocktailManagerMoq.Object);
                await sut.CreateBar(barForTest.ToDTO());

                Assert.AreEqual(1, assertContext.Bars.Count());
            }
        }

        [TestMethod]
        public async Task ThrowsException_WhenBar_AlreadyInDatabase()
        {
            var options = TestUtilities.GetOptions(nameof(ThrowsException_WhenBar_AlreadyInDatabase));
            var barForTest = new Bar()
            {
                Address = "Solunska 2",
                Information = "Information",
                MapDirections = "Go south",
                Name = "Bar",
                Picture = "Picture",
                Rating = 4.5d
            };

            var cocktailManagerMoq = new Mock<ICocktailManager>();

            using (var arrangeContext = new CMContext(options))
            {
                arrangeContext.Bars.Add(barForTest);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CMContext(options))
            {
                var sut = new BarManager(assertContext, cocktailManagerMoq.Object);
                await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => sut.CreateBar(barForTest.ToDTOforTests()));
            }
        }

        [TestMethod]
        public async Task ShowsCorrectMessage_WhenThrowsException()
        {
            var options = TestUtilities.GetOptions(nameof(ShowsCorrectMessage_WhenThrowsException));
            var barForTest = new Bar()
            {
                Address = "Solunska 2",
                Information = "Information",
                MapDirections = "Go south",
                Name = "Bar",
                Picture = "Picture",
                Rating = 4.5d
            };
            var message = "Bar already exists in the database!";

            var cocktailManagerMoq = new Mock<ICocktailManager>();

            using (var arrangeContext = new CMContext(options))
            {
                arrangeContext.Bars.Add(barForTest);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CMContext(options))
            {
                var sut = new BarManager(assertContext, cocktailManagerMoq.Object);
                var ex = await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => sut.CreateBar(barForTest.ToDTOforTests()));
                Assert.IsTrue(ex.Message.Contains(message));
            }
        }
    }
}
