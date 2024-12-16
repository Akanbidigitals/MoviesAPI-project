using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movies.Api.Auth;
using Movies.Api.Mapping;
using Movies.Application.Model;
using Movies.Application.Repositories;
using Movies.Application.Services;
using Movies.Contracts.Requests;


namespace Movies.Api.Controllers
{

    public class MoviesController : Controller
    {
        private readonly IMovieService _movieRepository;
        public MoviesController(IMovieService movieRepository)
        {
            _movieRepository = movieRepository;
        }
      //[Authorize(AuthConstants.AdminUserPolicyName)]
        [HttpPost(ApiEndpoints.Movies.Create)]
        public async Task<IActionResult> Create([FromBody] CreateMovieRequest request)
        {
            var movie = request.MapToMovie();

           var res =  await _movieRepository.CreateAsync(movie);
            return Ok(res);
        }

        [HttpGet(ApiEndpoints.Movies.Get)]
        public async Task <IActionResult> Get([FromRoute] string idOrSlug)
        {
            var userId = HttpContext.GetUserId();
            var movie = Guid.TryParse(idOrSlug, out var id)? await _movieRepository.GetByIdAsync(id,userId): 
                await _movieRepository.GetBySlug(idOrSlug, userId);
            if (movie is null)
            {
                return NotFound();
            }
            var response = movie.MapToResponse();
            return Ok(response);
        }

        
        [HttpGet(ApiEndpoints.Movies.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            var userId = HttpContext.GetUserId();

            var movies = await _movieRepository.GetAllAsync(userId);
            var moviesResponse = movies.MapToResponse();
            return Ok(moviesResponse);
        }
       // [Authorize(AuthConstants.AdminUserPolicyName)]
        [HttpPut(ApiEndpoints.Movies.Update)]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateMovieRequest request)
        {
            var userId = HttpContext.GetUserId();
            var movie = request.MapToMovie(id);
            var updatedMovie = await _movieRepository.UpdateAsync(movie,userId);
            if (updatedMovie is null)
            {
                return NotFound();
            }
            return Ok();

        }
       // [Authorize(AuthConstants.AdminUserPolicyName)]
        [HttpDelete(ApiEndpoints.Movies.Delete)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var deleted = await _movieRepository.DeleteByIdAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return Ok();
        }
    }   
}
