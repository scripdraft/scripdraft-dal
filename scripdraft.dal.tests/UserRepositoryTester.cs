using System;
using MongoDB.Driver;
using ScripDraft.Data;
using Xunit;

namespace ScripDraft.Tests.DAL
{
    [Collection("Test DB collection")]
    public class UserRepositoryTester
    {
        TestDBConnection _dBConnection = null;

        public UserRepositoryTester(TestDBConnection dbConnection)
        {
            _dBConnection = dbConnection;
        }

        [Fact]
        public void CanCreateUser()
        {
            IMongoDatabase db = _dBConnection.Connection;

            UserRepository userRepository = new UserRepository();
            userRepository.Database = db;

            
        }
    }
}
