using MediatR;
using MongoDB.Bson;
using MongoDB.Driver;
using Server.Application.Interface;
using Server.Data;
using Server.Domain.Model;

namespace Server.CQRS.Post.Commond
{
    public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, string>
    {
        private readonly IMongoCollection<PostDetails> _posts;
        private readonly ICloudinaryInterface _cloudinary;

        public CreatePostCommandHandler(MongoDbService dbService, ICloudinaryInterface cloudinary)
        {
            _posts = dbService.Posts;
            _cloudinary = cloudinary;
        }

        public async Task<string> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            // 1. Validate UserId
            if (string.IsNullOrWhiteSpace(request.Dto.UserId) ||
                !ObjectId.TryParse(request.Dto.UserId, out var userObjectId))
            {
                throw new ArgumentException("Invalid or missing UserId");
            }

            // 2. Upload Image
            string? imageUrl = null;
            if (request.Dto.PostPic != null)
            {
                imageUrl = await _cloudinary.UploadImageAsync(request.Dto.PostPic);
            }

            // 3. Upload Video
            string? videoUrl = null;
            if (request.Dto.PostVideo != null)
            {
                videoUrl = await _cloudinary.UploadVideoAsync(request.Dto.PostVideo);
            }

            // 4. Create Post
            var post = new PostDetails
            {
                UserId = userObjectId,
                PostPic = imageUrl,
                PostVideo = videoUrl,
                Caption = request.Dto.Caption,

                Comments = new List<CommentDetails>(), // Important to initialize
                LikedBy = new HashSet<string>(), // ✅ Fixed: was Dictionary, now HashSet

                Tags = request.Dto.Tags ?? new List<string>(),
                Visibility = request.Dto.Visibility,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _posts.InsertOneAsync(post);

            return "Post created successfully";
        }
    }
}
