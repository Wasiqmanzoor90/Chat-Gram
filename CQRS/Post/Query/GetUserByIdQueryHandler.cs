using MediatR;
using MongoDB.Bson;
using MongoDB.Driver;
using Server.CQRS.Post.Dtos;
using Server.CQRS.Post.Query;
using Server.Data;

namespace Server.CQRS.Post.Handlers
{
    public class GetAllPostsQueryHandler : IRequestHandler<GetPostsIdQuery, List<PostDto>>
    {
        private readonly MongoDbService _dbService;

        public GetAllPostsQueryHandler(MongoDbService dbService)
        {
            _dbService = dbService;
        }

        public async Task<List<PostDto>> Handle(GetPostsIdQuery request, CancellationToken cancellationToken)
        {
            // Step 1: Get all posts
            var posts = await _dbService.Posts
                .Find(_ => true) // No filter, get all posts
                .ToListAsync(cancellationToken);

            // Step 2: Extract unique UserIds from the posts
            var userIds = posts.Select(p => p.UserId).Distinct().ToList();

            // Step 3: Fetch users by UserIds
            var users = await _dbService.Users
                .Find(u => userIds.Contains(u.Id)) // Get users with the matching UserIds
                .ToListAsync(cancellationToken);

            var userMap = users.ToDictionary(u => u.Id, u => u.Name);

            // Step 4: Map posts to PostDto with user's Name
            return posts.Select(post => new PostDto
            {
                Id = post.Id.ToString(),
                UserId = post.UserId.ToString(),
                Name = userMap.ContainsKey(post.UserId) ? userMap[post.UserId] : "Unknown",  // Fetch user name from the map
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
