using MediatR;
using Server.CQRS.Comment.Dtos;

namespace Server.CQRS.Comment.Query
{
    public record GetCommentByIdQuery : IRequest<List<CreateCommentDto>>;
    public record GetCommentsByPostIdQuery(string PostId) : IRequest<List<CreateCommentDto>>;

}
