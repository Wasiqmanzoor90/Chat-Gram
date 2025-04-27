using MongoDB.Bson;

namespace Server.Domain.Model
{
    public class CommentDetails
    {
        public ObjectId Id { get; set; }
        public ObjectId PostId { get; set; }  //Relation with Post
        public ObjectId UserId { get; set; }  //Relation with User
        public string? Content { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
        public List<Object> Likes { get; set; } = [];

    }
}
