using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using ScripDraft.Data.Settings;

namespace ScripDraft.Data
{
    public class SDDatabase
    {
        private MongoClient _client = null;
        IMongoDatabase _database = null;

        public SDDatabase(IConfiguration configuration)
        {
            _client = new MongoClient(DatabaseSettings.ReadConnectionString(configuration));
            _database = _client.GetDatabase(DatabaseSettings.ReadDatabaseName(configuration));
        }

        public IMongoDatabase Database => _database;
    }
}