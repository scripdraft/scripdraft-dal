using System;
using System.Collections.Generic;
using MongoDB.Driver;
using ScripDraft.Data;
using ScripDraft.Data.Entities;
using Xunit;

namespace ScripDraft.Tests.DAL
{
    [Collection("Test DB collection")]
    public class UserRepositoryTester
    {
        private TestDBConnection _dBConnection = null;
        private IMongoDatabase _database = null;
        private UserRepository _userRepository = null;

        public UserRepositoryTester(TestDBConnection dbConnection)
        {
            _dBConnection = dbConnection;
            _database = _dBConnection.Connection;
            _userRepository = new UserRepository();
            _userRepository.Database = _database;
        }

        [Fact]
        public async void CanCreateUser()
        {
            User expectedUser = new User() { Id = Guid.NewGuid(), Name = "test1", Username = "test1", Email = "test1@a.au" };
            await _userRepository.UpsertAsync(expectedUser);

            User actualUser = await _userRepository.LoadAsync(expectedUser.Id);

            await _userRepository.DeleteAsync(expectedUser.Id);

            Assert.Equal(expectedUser.Id, actualUser.Id);            
        }

        [Fact]
        public async void CanLoadMultipleUsers()
        {
            User user1 = new User() { Id = Guid.NewGuid(), Name = "test1", Username = "test1", Email = "test1@a.au" };
            User user2 = new User() { Id = Guid.NewGuid(), Name = "user2", Username = "user2", Email = "user2@a.au" };

            await _userRepository.UpsertAsync(user1);
            await _userRepository.UpsertAsync(user2);

            List<User> users = await _userRepository.LoadAsync();

            await _userRepository.DeleteAsync(user1.Id);
            await _userRepository.DeleteAsync(user2.Id);

            Assert.True(users.Count == 2);
        }

        [Fact]
        public async void CanLoadUserByUsername()
        {
            User expectedUser = new User() { Id = Guid.NewGuid(), Name = "test1", Username = "test1_user", Email = "test1@a.au" };

            await _userRepository.UpsertAsync(expectedUser);

            User actualUser = await _userRepository.LoadByUsernameAsync(expectedUser.Username);

            await _userRepository.DeleteAsync(actualUser.Id);

            Assert.Equal(expectedUser.Id, actualUser.Id);
        }
    }
}
