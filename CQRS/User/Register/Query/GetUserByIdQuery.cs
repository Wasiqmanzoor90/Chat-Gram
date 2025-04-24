using MediatR;
using MongoDB.Bson;
using Server.CQRS.User.Register.Dtos;

namespace Server.CQRS.User.Register.Query
{
    public record GetUserByIdQuery(ObjectId Id) : IRequest<UserDto>;

}
