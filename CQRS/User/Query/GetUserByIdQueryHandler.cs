using MediatR;
using MongoDB.Bson;
using MongoDB.Driver;
using Server.CQRS.User.Dtos;
using Server.Data;

namespace Server.CQRS.User.Query
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto>
    {
        private readonly MongoDbService _dbService;

        public GetUserByIdQueryHandler(MongoDbService dbService)
        {
            _dbService = dbService;
        }

        public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {



            var user = await _dbService.Users
     .Find(u => u.UserId == request.UserId)  // Now this looks for the separate UserId field
     .FirstOrDefaultAsync(cancellationToken);
            if (user == null)
            {
                throw new ("User Not found");
            }

            return new UserDto
            {
                UserId = user.UserId,
                Name = user.Name,
                Email = user.Email,
              
            };
        }

    }
}