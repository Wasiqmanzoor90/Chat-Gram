using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Server.CQRS.Comment.Commond;
using Server.CQRS.Comment.Dtos;
using Server.CQRS.Post.Commond;
using Server.CQRS.Post.Dtos;
using Server.CQRS.Post.Query;
using Server.CQRS.User.Login.Commond;
using Server.CQRS.User.Register.Commond;
using Server.CQRS.User.Register.Query;

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
        public async Task<IActionResult> Login(CreateLoginCommond login)
        {
            var token = await _mediator.Send(login);
            return Ok(token);
        }


        [Authorize]
        [HttpGet("GetPostsByUser")]
        public async Task<IActionResult> GetPostsByUser([FromQuery] string userId)
        {
            var objectId = new ObjectId(userId);
            var result = await _mediator.Send(new GetPostsIdQuery(objectId));
            return Ok(result);
        }
        

        [HttpPost("CreatePost")]
        public async Task<IActionResult> CreatePost([FromForm] CreatePostDto dto)
        {
            var result = await _mediator.Send(new CreatePostCommand(dto));
            return Ok(new { PostId = result });
        }

        [Authorize]
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