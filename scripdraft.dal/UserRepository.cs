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
        private const string CollectionName = "users";
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
            var result = await _users.DeleteOneAsync(filter);
        }

        public async Task UpsertAsync(User entity) 
        { 
            await _users.ReplaceOneAsync(new BsonDocument("_id", entity.Id), entity, new UpdateOptions { IsUpsert = true });
        }

        public async Task<List<User>> LoadAsync() => (await _users.FindAsync(user => true)).ToList();

        public async Task<User> LoadAsync(Guid id) => (await _users.FindAsync(user => user.Id == id)).FirstOrDefault();

        public async Task<User> LoadByUsernameAsync(string username) => (await _users.FindAsync(user => user.UserName == username)).FirstOrDefault();
    }
}