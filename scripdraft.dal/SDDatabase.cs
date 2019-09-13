using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using ScripDraft.Data.Settings;

namespace ScripDraft.Data
{
    public class SDDatabase
    {
        public static IMongoDatabase GetDatabase(IConfiguration configuration)
        {
            MongoClient _client = new MongoClient(DatabaseSettings.ReadConnectionString(configuration));
            IMongoDatabase _database = _client.GetDatabase(DatabaseSettings.ReadDatabaseName(configuration));

            return _database;
        }
    }
}