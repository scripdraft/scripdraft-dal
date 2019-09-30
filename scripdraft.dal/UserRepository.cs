using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using ScripDraft.Data.Entities;

namespace ScripDraft.Data
{
    public class UserRepository : IRepository<User>
    {
        private const string CollectionName = "Users";
        private IMongoCollection<User> _users;
        private IMongoDatabase _database = null;

        public IMongoDatabase Database 
        { 
            get
            {
                return _database;
            }
            set
            {
                _database = value;                
                if(_users == null)
                {
                    _users = _database.GetCollection<User>(CollectionName);
                }
            }
        }

        public async Task DeleteAsync(Guid id)
        {            
            var filter = new BsonDocument("_id", id);
            var result = await _users.FindOneAndDeleteAsync(filter);
        }

        public async Task InsertAsync(User entity) => await _users.InsertOneAsync(entity);

        public async Task<List<User>> LoadAsync() => (await _users.FindAsync(user => true)).ToList();

        public async Task<User> LoadAsync(Guid id) => (await _users.FindAsync(user => user.Id == id)).FirstOrDefault();

        public async Task UpdateAsync(Guid id, User entity) => await _users.ReplaceOneAsync(filter: new BsonDocument("_id", id), replacement: entity);
    }
}