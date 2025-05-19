using MediatR;

namespace Server.CQRS.Post.Commond
{
    public class CreateLikeCommand : IRequest<bool>
    {
        public string PostId { get; set; }
        public string UserId { get; set; }

        public CreateLikeCommand(string postId, string userId)
        {
            PostId = postId;
            UserId = userId;
        }
    }
}
