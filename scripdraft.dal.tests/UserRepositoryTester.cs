using System;
using System.Collections.Generic;
using MongoDB.Driver;
using ScripDraft.Data;
using ScripDraft.Data.Entities;
using ScripDraft.WebApi.Models;
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
            UserModel expectedUserModel = new UserModel() { Id = Guid.NewGuid(), Name = "test1", Username = "test1", Password = "test1", Email = "test1@a.au" };
            User expectedUser = UserModel.CreateEntity(expectedUserModel);
            await _userRepository.UpsertAsync(expectedUser);

            User actualUser = await _userRepository.LoadAsync(expectedUser.Id);

            await _userRepository.DeleteAsync(expectedUser.Id);

            Assert.Equal(expectedUser.Id, actualUser.Id);            
        }

        [Fact]
        public async void CanLoadMultipleUsers()
        {
            UserModel userModel1 = new UserModel() { Id = Guid.NewGuid(), Name = "test1", Username = "test1", Password = "test1", Email = "test1@a.au" };
            UserModel userModel2 = new UserModel() { Id = Guid.NewGuid(), Name = "user2", Username = "user2", Password = "user2", Email = "user2@a.au" };
            User user1 = UserModel.CreateEntity(userModel1);
            User user2 = UserModel.CreateEntity(userModel2);

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
            UserModel expectedUserModel = new UserModel() { Id = Guid.NewGuid(), Name = "test1", Username = "test1_user", Password = "test1", Email = "test1@a.au" };
            User expectedUser = UserModel.CreateEntity(expectedUserModel);

            await _userRepository.UpsertAsync(expectedUser);

            User actualUser = await _userRepository.LoadByUsernameAsync(expectedUser.Username);

            await _userRepository.DeleteAsync(actualUser.Id);

            Assert.Equal(expectedUser.Id, actualUser.Id);
        }

        [Fact]
        public async void CanLoadByUsernamePassword()
        {
            UserModel expectedUserModel = new UserModel() { Id = Guid.NewGuid(), Name = "test1", Username = "test1_user", Password = "test1", Email = "test1@a.au" };
            User expectedUser = UserModel.CreateEntity(expectedUserModel);

            await _userRepository.UpsertAsync(expectedUser);

            User actualUser = await _userRepository.LoadByUsernamePasswordAsync(expectedUser.Username, expectedUser.Password);

            await _userRepository.DeleteAsync(actualUser.Id);

            Assert.Equal(expectedUser.Id, actualUser.Id);
        }
    }
}
