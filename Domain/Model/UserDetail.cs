    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    using System.ComponentModel.DataAnnotations;

    namespace Server.Domain.Model
    {
        public class UserDetail
        {

        [BsonId]
        [BsonElement("_id")]
        public ObjectId Id { get; set; }  // This maps to _id

        [BsonElement("UserId")]
        public ObjectId UserId { get; set; }  // This maps to UserId

        public required string Name { get; set; }
            [EmailAddress]
            public required string Email { get; set; }
            public required string Password { get; set; }
            public string? ProfilePic { get; set; }
            public string? Phone { get; set; }
            public List<ObjectId> Followers { get; set; } = [];
            public List<ObjectId> Following { get; set; } = [];
            public DateTime DateCreated { get; set; } = DateTime.UtcNow;
            public DateTime? DateModified { get; set; } = DateTime.UtcNow;
        }
    }
