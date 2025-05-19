using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Server.CQRS.Comment.Query;
using Server.CQRS.Comment.Commond;
using Server.CQRS.Comment.Dtos;
using Server.CQRS.Post.Commond;
using Server.CQRS.Post.Dtos;
using Server.CQRS.Post.Query;
using Server.CQRS.User.Login.Commond;
using Server.CQRS.User.Register.Commond;
using Server.CQRS.User.Register.Query;
using System.Security.Claims;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<UserController> _logger; // Added logger for debugging

        public UserController(IMediator mediator, ILogger<UserController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] CreateUserCommond commond)
        {
            var userid = await _mediator.Send(commond);
            return Ok(new { UserId = userid, Message = "User Created Sucessfully!" });
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] CreateLoginCommond login)
        {
            // Send the login request to the handler
            var loginDto = await _mediator.Send(login);

            _logger.LogInformation("Logging in user with email: {Email}", login.Email);

            // Set the JWT token as a cookie
            Response.Cookies.Append("token", loginDto.Token, new CookieOptions
            {
                HttpOnly = true,
                Secure = HttpContext.Request.IsHttps, // Automatically set based on connection
                SameSite = SameSiteMode.Lax, // Changed from None to Lax for better compatibility
                Expires = DateTime.UtcNow.AddDays(1)
            });

            // Return success response
            return Ok(loginDto);
        }

        [HttpGet("GetPostsByUser")]
        public async Task<IActionResult> GetAllPosts()
        {
            var result = await _mediator.Send(new GetPostsIdQuery());
            return Ok(result);
        }

        [Authorize]
        [HttpGet("Verify")]
        public IActionResult Verify()
        {
           
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var name = User.FindFirst(ClaimTypes.Name)?.Value;
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
       

            return Ok(new { message = "Token is valid", email, name, userId });
        }


        [Authorize]
        [HttpPost("LikeBy/{postId}")]
        public async Task<IActionResult> ToggleLike(string postId, [FromBody] string userId)
        {
            var result = await _mediator.Send(new CreateLikeCommand(postId, userId));
            return result ? Ok("Like toggled successfully.") : NotFound("Post or user not found.");
        }


        [HttpGet("GetComment/{postId}")]
        public async Task<IActionResult> GetComment(string postId)
        {
            try
            {
                var comments = await _mediator.Send(new GetCommentsByPostIdQuery(postId));

                if (comments?.Count == 0)
                    return NotFound("No comments found for this post.");

                return Ok(comments);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching comments for post {PostId}", postId);
                return StatusCode(500, "An error occurred while fetching comments.");
            }
        }

        [HttpPost("CreatePost")]
        public async Task<IActionResult> CreatePost([FromForm] CreatePostDto dto)
        {
            var result = await _mediator.Send(new CreatePostCommand(dto));
            return Ok(new { PostId = result });
        }

        [HttpPost("CreateComment")]
        public async Task<IActionResult> CreateComment([FromBody] CreateCommentDto dto)
        {
            var result = await _mediator.Send(new CreateCommentCommond(dto));
            return Ok(new { CommentId = result });
        }

        [Authorize]
        [HttpGet("Details")]
        public async Task<IActionResult> Detail([FromQuery] string userId)
        {
            if (!ObjectId.TryParse(userId, out ObjectId objectId))
                return BadRequest("Invalid user ID format.");
            var result = await _mediator.Send(new GetUserByIdQuery(objectId));
            return Ok(result);
        }
    }
}