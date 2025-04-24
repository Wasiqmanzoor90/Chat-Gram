using MediatR;
using MongoDB.Bson;
using MongoDB.Driver;
using Server.CQRS.User.Register.Dtos;
using Server.Data;

namespace Server.CQRS.User.Register.Query
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
                  .Find(u => u.Id == request.Id)
                  .FirstOrDefaultAsync(cancellationToken);
            if (user == null)
            {
                throw new("User Not found");
            }
            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
            };
        }

    }
}