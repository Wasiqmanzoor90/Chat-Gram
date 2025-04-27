using MediatR;
using MongoDB.Bson;
using Server.CQRS.Post.Dtos;


namespace Server.CQRS.Post.Query
{
    public record GetPostsIdQuery(ObjectId UserId) : IRequest<List<PostDto>>;

}
