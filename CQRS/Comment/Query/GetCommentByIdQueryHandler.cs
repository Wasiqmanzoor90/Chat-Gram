using MediatR;
using MongoDB.Driver;
using Server.CQRS.Comment.Dtos;
using Server.CQRS.Comment.Query;
using Server.Data;

public class GetCommentsByPostIdQueryHandler : IRequestHandler<GetCommentsByPostIdQuery, List<CreateCommentDto>>
{
    private readonly MongoDbService _dbService;

    public GetCommentsByPostIdQueryHandler(MongoDbService dbService)
    {
        _dbService = dbService;
    }

    public async Task<List<CreateCommentDto>> Handle(GetCommentsByPostIdQuery request, CancellationToken cancellationToken)
    {
        if (!MongoDB.Bson.ObjectId.TryParse(request.PostId, out var postObjectId))
        {
            throw new ArgumentException("Invalid PostId format.");
        }

        var post = await _dbService.Posts
            .Find(p => p.Id == postObjectId)
            .FirstOrDefaultAsync(cancellationToken);

        if (post == null || post.Comments == null)
            return new List<CreateCommentDto>();

        var commentDtos = new List<CreateCommentDto>();

        foreach (var comment in post.Comments)
        {
            var user = await _dbService.Users
                .Find(u => u.Id == comment.UserId)
                .FirstOrDefaultAsync(cancellationToken);

            commentDtos.Add(new CreateCommentDto
            {
                PostId = request.PostId,
                UserId = comment.UserId.ToString(),
                Name = user?.Name ?? "Unknown",
                Content = comment.Content ?? string.Empty,
                Created = comment.CreatedAt ?? DateTime.UtcNow
            });
        }

        return commentDtos;
    }
}
