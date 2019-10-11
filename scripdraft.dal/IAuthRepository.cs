using System.Threading.Tasks;
using MongoDB.Driver;
using ScripDraft.Data.Entities;

namespace ScripDraft.Data
{
    public interface IAuthRepository
    {
        IMongoDatabase Database { get; set; }
        
        Task<User> Register(User user, string password);
        Task<User> Login(string username, string password);
        Task<bool> UserExists(string username);
    }
}