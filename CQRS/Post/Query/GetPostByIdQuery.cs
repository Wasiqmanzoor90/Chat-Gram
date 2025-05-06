using MediatR;
using Server.CQRS.Post.Dtos;
using System.Collections.Generic;

namespace Server.CQRS.Post.Query
{
    public class GetPostsIdQuery : IRequest<List<PostDto>> { }
}
