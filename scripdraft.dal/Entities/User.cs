using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace ScripDraft.Data.Entities
{
    public class User
    {
        [BsonId]
        public Guid Id { get; set; }
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("username")]
        public string Username { get; set; }
        [BsonElement("password")]
        public byte[] PasswordHash { get; set; }
        [BsonElement("password_salt")]
        public byte[] PasswordSalt { get; set; }
        [BsonElement("email")]
        public string Email { get; set; }
    }
}