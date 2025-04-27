using Server.Types;

namespace Server.CQRS.Post.Dtos
{
    public class PostDto
    {
        public string Id { get; set; } = string.Empty; // Important! Needed for fetching
        public string UserId { get; set; } = string.Empty;
        public string? PostPicUrl { get; set; } // URL or Path to image
        public string? PostVideoUrl { get; set; }
        public string? Caption { get; set; }
        public List<string> Tags { get; set; } = new();
        public Visibility Visibility { get; set; } = Visibility.Public;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
