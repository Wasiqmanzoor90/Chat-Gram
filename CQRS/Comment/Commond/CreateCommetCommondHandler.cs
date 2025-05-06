using MediatR;
using MongoDB.Bson;
using MongoDB.Driver;
using Server.Data;
using Server.Domain.Model;

namespace Server.CQRS.Comment.Commond
{
    public class CreateCommetCommondHandler : IRequestHandler<CreateCommentCommond, ObjectId>
    {
        private readonly MongoDbService _dbservice;
        public CreateCommetCommondHandler(MongoDbService dbService)
        {
            _dbservice = dbService;
        }

        public async Task<ObjectId> Handle(CreateCommentCommond request, CancellationToken cancellationToken)
        {

            // Check if PostId is a valid ObjectId
            if (!ObjectId.TryParse(request.dto.PostId, out var postObjectId))
                throw new ArgumentException("PostId is required and must be a valid ObjectId.");

            // Check if UserId is a valid ObjectId
            if (!ObjectId.TryParse(request.dto.UserId, out var userObjectId))
                throw new ArgumentException("UserId is required and must be a valid ObjectId.");

            // Create the comment
            var comment = new CommentDetails
            {
                UserId = userObjectId,
                PostId = postObjectId,
                Content = request.dto.Content,
                CreatedAt = DateTime.UtcNow,
                Likes = new List<Object>() // empty likes array for now
            };

            // Insert the comment into the Comments collection
            await _dbservice.Comments.InsertOneAsync(comment);

            // Update the post with the new comment in the Comments array
            var update = Builders<PostDetails>.Update.Push(p => p.Comments, comment);
            await _dbservice.Posts.UpdateOneAsync(
                p => p.Id == postObjectId,
                update,
                cancellationToken: cancellationToken
            );

            // Return the comment ID
            return comment.Id;

        }
    }
}
