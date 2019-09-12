using ScripDraft.Data.Entities;

namespace ScripDraft.WebApi.Models
{
    public class UserModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        internal static User CreateEntity(UserModel model)
        {
            User entity = new User()
            {
                Id = model.Id,
                Name = model.Name,
                UserName = model.UserName,
                Password = model.Password,
                Email = model.Email
            };

            return entity;
        }
    }
}