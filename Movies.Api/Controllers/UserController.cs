using Microsoft.AspNetCore.Mvc;
using Movies.Api.Mapping;
using Movies.Application.Model;
using Movies.Application.Repositories;
using Movies.Contracts.Requests;

namespace Movies.Api.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _ctx;
        public UserController(IUserRepository ctx)
        {
            _ctx = ctx;
        }

        [HttpPost(ApiEndpoints.User.Create)]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
        {
            var user = request.MapToUser();
           var res = await _ctx.CreateUser(user);
            return Ok(res);
        }

        [HttpGet(ApiEndpoints.User.Get)]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var user = await _ctx.GetUser(id);
            if (user is null)
            {
                return NotFound();
            }
            var response = user.MaptoUserResponse();
            return Ok(response);
        }
        [HttpGet(ApiEndpoints.User.Getall)]
        public async Task<IActionResult> Getall()
        {
            var users = await _ctx.GetAllUsers();
            var response = users.MapToUserResponse();
            return Ok(response);
        }
        [HttpGet("Token")]
        public async Task<IActionResult> GetToken( Guid id)
        {
            var token = await _ctx.Token(id);
            return Ok(token);
        }
    }
}
