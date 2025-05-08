using MediatR;
using MongoDB.Driver;
using Server.CQRS.Comment.Dtos;
using Server.CQRS.Comment.Query;
using Server.Data;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

public class GetCommentsByPostIdQueryHandler : IRequestHandler<GetCommentsByPostIdQuery, List<CreateCommentDto>>
{
    private readonly MongoDbService _dbService;

    public GetCommentsByPostIdQueryHandler(MongoDbService dbService)
    {
        _dbService = dbService;
    }

    public async Task<List<CreateCommentDto>> Handle(GetCommentsByPostIdQuery request, CancellationToken cancellationToken)
    {
        // Log the incoming postId for debugging
        Console.WriteLine($"Fetching comments for postId: {request.PostId}");

        // Check if PostId can be parsed to ObjectId
        if (!MongoDB.Bson.ObjectId.TryParse(request.PostId, out var postObjectId))
        {
            // Log invalid postId format
            Console.WriteLine($"Invalid PostId format: {request.PostId}");
            throw new ArgumentException("Invalid PostId format.");
        }

        // Query to fetch the post using postObjectId
        var post = await _dbService.Posts
            .Find(p => p.Id == postObjectId)
            .FirstOrDefaultAsync(cancellationToken);

        // If no post found or no comments, return empty list
        if (post == null)
        {
            Console.WriteLine($"Post with id {request.PostId} not found.");
            return new List<CreateCommentDto>();  // Empty list if no comments
        }

        if (post.Comments == null || post.Comments.Count == 0)
        {
            Console.WriteLine($"No comments found for postId: {request.PostId}");
            return new List<CreateCommentDto>();  // Empty list if no comments exist
        }

        // Process comments
        var commentDtos = new List<CreateCommentDto>();

        foreach (var comment in post.Comments)
        {
            // Fetch user details for each comment
            var user = await _dbService.Users
                .Find(u => u.Id == comment.UserId)
                .FirstOrDefaultAsync(cancellationToken);

            // Add each comment to the DTO list
            commentDtos.Add(new CreateCommentDto
            {
                PostId = request.PostId,
                UserId = comment.UserId.ToString(),
                Name = user?.Name ?? "Unknown",  // If user not found, use "Unknown"
                Content = comment.Content ?? string.Empty,
                Created = comment.CreatedAt ?? DateTime.UtcNow
            });
        }

        // Return the list of comment DTOs
        return commentDtos;
    }
}
