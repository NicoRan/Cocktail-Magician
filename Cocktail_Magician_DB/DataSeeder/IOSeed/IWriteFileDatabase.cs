using System.Collections.Generic;

namespace Cocktail_Magician_DB.DataSeeder.IOSeed
{
    public interface IWriteFileDatabase
    {
        void WriteJsonFile<T>(string address, IEnumerable<T> list);
    }
}