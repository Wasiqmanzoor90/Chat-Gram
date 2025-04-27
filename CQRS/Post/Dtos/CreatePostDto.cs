using Server.Types;

namespace Server.CQRS.Post.Dtos
{
    public class CreatePostDto
    {
        public string UserId { get; set; } = string.Empty;
        public IFormFile? PostPic { get; set; }
        public IFormFile? PostVideo { get; set; }
        public string? Caption { get; set; }
        public List<string> Tags { get; set; } = new();
        public Visibility Visibility { get; set; } = Visibility.Public;
    }
}
