using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movies.Api.Auth;
using Movies.Application.Services;
using Movies.Contracts.Requests;

namespace Movies.Api.Controllers
{
    public class RatingController : Controller
    {
        private readonly IRatingService _ratingService;
        public RatingController(IRatingService rating)
        {
            _ratingService = rating;             
        }
        [Authorize]
        [HttpPut(ApiEndpoints.Movies.Rate)]
        public async Task<IActionResult> RateMovie([FromRoute] Guid Id, [FromBody] RateMovieRequest  request) 
        {

            var userId = HttpContext.GetUserId();
            var result = await _ratingService.RateMovieAsync(Id, request.rating, userId!.Value);
            return result ? Ok(result) : NotFound();
        }
    }
}
