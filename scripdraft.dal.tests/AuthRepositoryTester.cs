using System;
using System.Threading.Tasks;
using MongoDB.Driver;
using ScripDraft.Data;
using ScripDraft.Data.Entities;
using Xunit;

namespace ScripDraft.Tests.DAL
{
    [Collection("Test DB collection")]
    public class AuthRepositoryTester
    {
        private TestDBConnection _dBConnection = null;
        private IMongoDatabase _database = null;
        private IAuthRepository _authRepository = null;
        private UserRepository _userRepository = null;
        
        public AuthRepositoryTester(TestDBConnection dbConnection)
        {
            _dBConnection = dbConnection;
            _database = _dBConnection.Connection;
            _authRepository = new AuthRepository();
            _authRepository.Database = _database;

            _userRepository = new UserRepository();
            _userRepository.Database = _database;
        }

        [Fact]
        public async Task CanRegisterUser()
        {
            string password = "CanRegisterUser";
            User expectedUser = new User() { Id = Guid.NewGuid(), Name = "CanRegisterUser", Username = "CanRegisterUser", Email = "CanRegisterUser@a.au" };
            
            User actualUser = await _authRepository.Register(expectedUser, password);

            Assert.True(_authRepository.UserExists(expectedUser.Username).Result);

            await _userRepository.DeleteAsync(actualUser.Id);
        }

        [Fact]
        public async Task CanLoginUser()
        {
            string username = "CanLoginUser";
            string password = "CanLoginUser";
            User expectedUser = new User() { Id = Guid.NewGuid(), Name = username, Username = "CanLoginUser", Email = "CanLoginUser@a.au" };
            
            User actualUser = await _authRepository.Register(expectedUser, password);

            Assert.True(_authRepository.UserExists(expectedUser.Username).Result);

            User loggedInUser = await _authRepository.Login(username, password);

            Assert.True(actualUser.Id == loggedInUser.Id);

            await _userRepository.DeleteAsync(actualUser.Id);
        }
    }
}