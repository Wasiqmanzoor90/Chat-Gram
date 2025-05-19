using MediatR;
using MongoDB.Bson;
using MongoDB.Driver;
using Server.Data;
using Server.Domain.Model;

namespace Server.CQRS.Post.Commond
{
    public class CreateLikeCommondHandler : IRequestHandler<CreateLikeCommand, bool>
    {
        private readonly IMongoCollection<PostDetails> _posts;
        private readonly IMongoCollection<UserDetail> _users;

        public CreateLikeCommondHandler(MongoDbService dbService)
        {
            _posts = dbService.Posts;
            _users = dbService.Users;
        }
        public async Task<bool> Handle(CreateLikeCommand request, CancellationToken cancellationToken)
        {
            if (!ObjectId.TryParse(request.PostId, out ObjectId postObjectId) ||
           !ObjectId.TryParse(request.UserId, out ObjectId userObjectId))
            {
                return false;
            }

            var post = await _posts.Find(p => p.Id == postObjectId).FirstOrDefaultAsync(cancellationToken);
            var user = await _users.Find(u=> u.Id == userObjectId).FirstOrDefaultAsync(cancellationToken);


            if (post == null || user == null)
                return false;

            post.LikedBy ??= new HashSet<string>();
            if(post.LikedBy.Contains(request.UserId))
                post.LikedBy.Remove(request.UserId); // Unlike
            else
                post.LikedBy.Add(request.UserId);    // Like

            post.UpdatedAt = DateTime.UtcNow;
            var updateres = await _posts.ReplaceOneAsync(
                p => p.Id == post.Id,
                post,
                cancellationToken: cancellationToken

                );

            return updateres.IsAcknowledged && updateres.ModifiedCount > 0;

        }

        
    }
}
