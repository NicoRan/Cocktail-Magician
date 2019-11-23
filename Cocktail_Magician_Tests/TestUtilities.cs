using Cocktail_Magician_DB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cocktail_Magician_Tests
{
    public static class TestUtilities
    {
        public static DbContextOptions<CMContext> GetOptions(string databaseName)
        {
            return new DbContextOptionsBuilder<CMContext>()
                .UseInMemoryDatabase(databaseName)
                .Options;
        }
    }
}
