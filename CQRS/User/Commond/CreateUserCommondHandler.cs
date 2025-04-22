using CloudinaryDotNet.Core;
using MediatR;
using MongoDB.Bson;
using MongoDB.Driver;
using Server.Data;

namespace Server.CQRS.User.Commond
{
    public class CreateUserCommondHandler : IRequestHandler<CreateUserCommond, ObjectId>
    {
        private readonly MongoDbService _dbService;
        public CreateUserCommondHandler(MongoDbService dbService)
            {
            _dbService = dbService;
            }


        public async Task<ObjectId> Handle(CreateUserCommond request, CancellationToken cancellationToken)
        {
          var finduser = await _dbService.Users.Find(u=> u.Email == request.Email).FirstOrDefaultAsync();
            if (finduser != null)
            {
                throw new Exception("Email already exists.");
            }

            var hash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var user = new Domain.Model.UserDetail
            {
                UserId = ObjectId.GenerateNewId(),
                Name = request.Name,
                Email = request.Email,
                Password = hash,
                ProfilePic = null,
                Phone = null,
                DateCreated = DateTime.UtcNow,
                DateModified = DateTime.UtcNow
            };

            await _dbService.Users.InsertOneAsync(user, cancellationToken: cancellationToken);

            return user.UserId;
        }
    }
    }

