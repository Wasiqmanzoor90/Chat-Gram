using MediatR;
using MongoDB.Bson;
using Server.CQRS.Comment.Dtos;

namespace Server.CQRS.Comment.Commond;

  public record CreateCommentCommond(CreateCommentDto dto):IRequest<ObjectId>;

