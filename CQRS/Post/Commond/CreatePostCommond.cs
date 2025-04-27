using MediatR;
using Server.CQRS.Post.Dtos;
namespace Server.CQRS.Post.Commond;


    public record CreatePostCommand(CreatePostDto Dto) : IRequest<string>; // returns Post ID

