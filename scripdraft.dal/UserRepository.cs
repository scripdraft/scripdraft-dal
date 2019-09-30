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

        public async Task Delete(Guid id)
        {            
            var filter = new BsonDocument("Id", id);
            var result = await _users.FindOneAndDeleteAsync(filter);
        }

        public async Task Insert(User entity) => await _users.InsertOneAsync(entity);

        public async Task<List<User>> Load() => (await _users.FindAsync(user => true)).ToList();

        public async Task<User> Load(Guid id) => (await _users.FindAsync(user => user.Id == id)).FirstOrDefault();

        public async Task Update(Guid id, User entity)
        {
            var result = await _users.ReplaceOneAsync(filter: new BsonDocument("Id", id), replacement: entity);

            return;
        }
    }
}