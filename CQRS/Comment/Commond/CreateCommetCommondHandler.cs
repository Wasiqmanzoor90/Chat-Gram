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

    

            // Validate UserId
            if (!ObjectId.TryParse(request.dto.UserId, out var userObjectId))
                throw new ArgumentException("UserId is required and must be a valid ObjectId.");

            // Validate PostId
            if (!ObjectId.TryParse(request.dto.PostId, out var postObjectId))
                throw new ArgumentException("PostId is required and must be a valid ObjectId.");


            var comment = new CommentDetails
            {
                UserId = userObjectId,
                PostId = postObjectId,
                Content = request.dto.Content,
                CreatedAt = DateTime.UtcNow,
                Likes = new List<Object>() // empty for now

            };

            await _dbservice.Comments.InsertOneAsync(comment);

            var update = Builders<PostDetails>.Update.Push(p => p.Comments, comment);
            await _dbservice.Posts.UpdateOneAsync(
                p => p.Id == postObjectId,
                update,
                  cancellationToken: cancellationToken
                );

            return comment.Id;
        }
    }
}
