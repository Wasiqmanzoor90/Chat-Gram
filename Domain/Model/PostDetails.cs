using MongoDB.Bson;
using Server.Types;

namespace Server.Domain.Model
{
    public class PostDetails
    {
        public ObjectId Id { get; set; }
        public string? PostPic {  get; set; }
        public string? PostVideo {  get; set; }
        public string? Caption {  get; set; }
        public ObjectId UserId { get; set; }  //Relation with User
        public List<CommentDetails> Comments { get; set; } = [];
        public List<string> Tags { get; set; } = [];
        public Visibility Visibility { get; set; } = Visibility.Public;
        public Dictionary<string, int> Reaction { get; set; } = [];
        public bool IsEdited { get; set; } = false; // Track if the post is edited
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
