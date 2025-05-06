namespace Server.CQRS.Comment.Dtos
{
    public class CreateCommentDto
    {
        public string UserId { get; set; } = string.Empty;
        public string PostId { get; set; } = string.Empty;
        public string? Name { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime Created { get; set; } = DateTime.UtcNow;
    }

}
