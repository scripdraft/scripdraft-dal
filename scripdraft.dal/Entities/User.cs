using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace ScripDraft.Data.Entities
{
    public class User
    {
        [BsonId]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}