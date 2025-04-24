using MediatR;
using MongoDB.Bson;

namespace Server.CQRS.User.Register.Commond
{
    public record CreateUserCommond(string Name, string Email, string Password) : IRequest<ObjectId>;
}
