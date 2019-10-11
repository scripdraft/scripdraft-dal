using System;
using System.Threading.Tasks;
using MongoDB.Driver;
using ScripDraft.Data.Entities;

namespace ScripDraft.Data
{
    public class AuthRepository : IAuthRepository
    {
        private UserRepository _userRepository = null;
        public IMongoDatabase Database 
        {  
            set
            {
                _userRepository = new UserRepository();
                _userRepository.Database = value;
            }

            get
            {
                return _userRepository is null ? null : _userRepository.Database;
            }
        }

        public async Task<User> Login(string username, string password)
        {
            User user = await _userRepository.LoadByUsernameAsync(username);
            if(user is null is false)
            {
                if(!PasswordValid(password, user))
                {
                    user = null;    // password does not match, return null
                }
            }

            return user;
        }

        private bool PasswordValid(string password, User user)
        {
            bool result = true;

            using(var hmac = new System.Security.Cryptography.HMACSHA512(user.PasswordSalt))
            { 
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)); // Create hash using password salt.

                for (int i = 0; i < computedHash.Length; i++)
                { 
                    // check if bytes are matching
                    if(computedHash[i] != user.PasswordHash[i]) 
                    {
                        result = false; // if mismatch
                        break;
                    }
                }    
            }

            return result;
        }

        public async Task<User> Register(User user, string password)
        {
            Tuple<byte[], byte[]> hashAndSalt = CreatePasswordHashAndSalt(password);
            user.PasswordHash = hashAndSalt.Item1; 
            user.PasswordSalt = hashAndSalt.Item2;

            await _userRepository.UpsertAsync(user);

            return user;
        }

        private Tuple<byte[], byte[]> CreatePasswordHashAndSalt(string password)
        {
            byte[] passwordHash; 
            byte[] passwordSalt;

            using(var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }

            return new Tuple<byte[], byte[]>(passwordHash, passwordSalt);
        }

        public async Task<bool> UserExists(string username)
        {
            User user = await _userRepository.LoadByUsernameAsync(username);

            return user is null is false;
        }
    }
}