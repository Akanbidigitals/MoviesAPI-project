using Microsoft.AspNetCore.Mvc;
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
        public Task<IActionResult> CreateUser([FromBody]CreateUserRequest user)
        {

        }
    }
}
