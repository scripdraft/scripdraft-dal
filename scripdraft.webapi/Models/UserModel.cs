using ScripDraft.Data.Entities;
using System;
using System.Security.Cryptography;
using System.Text;

namespace ScripDraft.WebApi.Models
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        internal static User CreateEntity(UserModel model) =>
            new User()
            {
                Id = model.Id.ToString().Equals("00000000-0000-0000-0000-000000000000") ? Guid.NewGuid() : model.Id,
                Name = model.Name,
                Username = model.Username,
                Password = EncryptPassword(model.Password),
                Email = model.Email
            };

        private static string EncryptPassword(string password)
        {
            string hashedPassword = string.Empty;

            using (SHA1CryptoServiceProvider provider = new SHA1CryptoServiceProvider())
            {
                var data = Encoding.UTF8.GetBytes(password);
                hashedPassword = Convert.ToBase64String(provider.ComputeHash(data));
            }

            return hashedPassword;
        }
    }
}