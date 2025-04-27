using MediatR;
using MongoDB.Bson;
using MongoDB.Driver;
using Server.CQRS.Post.Dtos;
using Server.CQRS.Post.Query;
using Server.Data;


namespace Server.CQRS.Post.Handlers
{
    public class GetPostsByUserIdQueryHandler : IRequestHandler<GetPostsIdQuery, List<PostDto>>
    {
        private readonly MongoDbService _dbService;

        public GetPostsByUserIdQueryHandler(MongoDbService dbService)
        {
            _dbService = dbService;
        }

        public async Task<List<PostDto>> Handle(GetPostsIdQuery request, CancellationToken cancellationToken)
        {

            // Ensure userId is valid and not empty
            if (request.UserId == ObjectId.Empty)
            {
                throw new Exception("Invalid or empty UserId.");
            }

            var posts = await _dbService.Posts
                .Find(p => p.UserId == request.UserId)
                .ToListAsync(cancellationToken);

            return posts.Select(post => new PostDto
            {
                Id = post.Id.ToString(),
                UserId = post.UserId.ToString(),
                PostPicUrl = post.PostPic,
                PostVideoUrl = post.PostVideo,
                Caption = post.Caption,
                Tags = post.Tags,
                Visibility = post.Visibility,
                CreatedAt = post.CreatedAt,
                UpdatedAt = post.UpdatedAt
            }).ToList();
        }
    }
}
