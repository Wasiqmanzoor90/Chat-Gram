using MediatR;
using Server.CQRS.Post.Dtos;
namespace Server.CQRS.Post.Commond;


    public record CreatePostCommand(PostDto Dto) : IRequest<string>; // returns Post ID

