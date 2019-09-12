using System.Collections.Generic;
using System.Threading.Tasks;
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

        public Task Delete(string id)
        {            
            throw new System.NotImplementedException();
        }

        public async Task Insert(User entity)
        {
            await _users.InsertOneAsync(entity);
        }

        public async Task<List<User>> Load()
        {
            List<User> loadedUsers = (await _users.FindAsync(user => true)).ToList();

            return loadedUsers;
        }

        public Task<User> Load(string id)
        {
            throw new System.NotImplementedException();
        }

        public Task Update(string id, User entity)
        {
            throw new System.NotImplementedException();
        }
    }
}