using MediatR;
using MongoDB.Bson;
using Server.CQRS.User.Dtos;

namespace Server.CQRS.User.Query
{
    public record GetUserByIdQuery(ObjectId UserId) : IRequest<UserDto>;

}
