using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using ScripDraft.Data.Settings;

namespace ScripDraft.Data
{
    public static class SDDatabase
    {
        private static MongoClient _client = null;
        private static IMongoDatabase _database = null;

        public static IMongoDatabase GetDatabase(IConfiguration configuration)
        {
            if(_client is null) InitMongoClient(configuration);
            
            _database = _client.GetDatabase(DatabaseSettings.ReadDatabaseName(configuration));

            return _database;
        }

        public async static Task DropDatabase(IConfiguration configuration)
        {
            if(_client is null) InitMongoClient(configuration);

            await _client.DropDatabaseAsync(DatabaseSettings.ReadDatabaseName(configuration));
        }

        private static void InitMongoClient(IConfiguration configuration)
        {
            _client = new MongoClient(DatabaseSettings.ReadConnectionString(configuration)); 
        }
    }
}