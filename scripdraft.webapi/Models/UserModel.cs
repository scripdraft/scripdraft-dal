using ScripDraft.Data.Entities;
using System;

namespace ScripDraft.WebApi.Models
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        internal static User CreateEntity(UserModel model) =>
            new User()
            {
                Id = model.Id.ToString().Equals("00000000-0000-0000-0000-000000000000") ? Guid.NewGuid() : model.Id,
                Name = model.Name,
                UserName = model.UserName,
                Password = model.Password,
                Email = model.Email
            };
    }
}