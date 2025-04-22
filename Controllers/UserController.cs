using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Server.CQRS.User.Commond;
using Server.CQRS.User.Query;

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