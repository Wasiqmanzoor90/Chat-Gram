using MediatR;
using MongoDB.Bson;

namespace Server.CQRS.User.Commond
{
  public record CreateUserCommond(string Name, string Email, string Password):IRequest<ObjectId>;  
}
