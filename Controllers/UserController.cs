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
        public UserController(IMediator mediator)
        {
           _mediator = mediator;
        }


        [HttpPost("Register")]
        public async Task <IActionResult> Register([FromBody] CreateUserCommond commond)
        {
            var userid = await _mediator.Send(commond);
            return Ok(new { UserId = userid, Message = "User Created Sucessfully!" });
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] CreateLoginCommond login)
        {
            // Send the login request to the handler to get the LoginDto response
            var loginDto = await _mediator.Send(login);

            // Return the LoginDto with the response
            return Ok(loginDto); // This will include the token, userId, email, and name
        }




        [HttpGet("GetPostsByUser")]
        public async Task<IActionResult> GetAllPosts()
        {
            var result = await _mediator.Send(new GetPostsIdQuery());
            return Ok(result);
        }



        [HttpGet("Verify")]
        public IActionResult Verify()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var name = User.FindFirst(ClaimTypes.Name)?.Value;

            return Ok(new
            {
                message = "Token is valid",
                email,
                name
            });
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
            catch
            {
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