using Server.Types;

namespace Server.CQRS.Post.Dtos
{
    public class PostDto
    {
        public string UserId { get; set; } = string.Empty; // Move it to top ✅
        public IFormFile? PostPic { get; set; }
        public IFormFile? PostVideo { get; set; }
        public string? Caption { get; set; }
        public List<string> Tags { get; set; } = new();
        public Visibility Visibility { get; set; } = Visibility.Public;
    }
}
