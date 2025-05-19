using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Server.Types;

namespace Server.Domain.Model
{
    [BsonIgnoreExtraElements]
    public class PostDetails
    {
        public ObjectId Id { get; set; }
        public string? PostPic { get; set; }
        public string? PostVideo { get; set; }
        public string? Caption { get; set; }
        public ObjectId UserId { get; set; }  // Relation with User
        public string? Name { get; set; }  // This is the field that will store the user's name
        public List<CommentDetails> Comments { get; set; } = new List<CommentDetails>();
        public List<string> Tags { get; set; } = new List<string>();
        public Visibility Visibility { get; set; } = Visibility.Public;
        public HashSet<string> LikedBy { get; set; } = new HashSet<string>(); // List of User IDs who liked

        public bool IsEdited { get; set; } = false; // Track if the post is edited
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
