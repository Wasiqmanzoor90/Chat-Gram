using MediatR;
using Server.CQRS.User.Login.Dtos;

namespace Server.CQRS.User.Login.Commond
{
   public record CreateLoginCommond(string Email, string Password): IRequest<LoginDto>;
}
