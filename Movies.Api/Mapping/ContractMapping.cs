using Movies.Application.Model;
using Movies.Contracts.Requests;
using Movies.Contracts.Responses;

namespace Movies.Api.Mapping
{
    public static class ContractMapping
    {
        public static Movie MapToMovie (this CreateMovieRequest request)
        {
           return  new Movie
            {
                
                Title = request.Title,
                YearOfRelease = request.YearOfRelease,
                Genres = request.Genres.ToList()

            };
        }  
        

        public static Movie MapToMovie (this UpdateMovieRequest request,Guid id)
        {
           return  new Movie
            {
                Id = id,
                Title = request.Title,
                YearOfRelease = request.YearOfRelease,
                Genres = request.Genres.ToList()

            };
        }
        public static MovieResponse MapToResponse(this Movie movie)
        {
            return new MovieResponse
            {
                Id = movie.Id,
                Title = movie.Title,
                Rating = movie.Rating,
                UserRating = movie.UserRating,
                Slug = movie.Slug,
                YearOfRelease = movie.YearOfRelease,
                Genres = movie.Genres.ToList()
            };
        }
        public static MoviesResponse MapToResponse(this IEnumerable<Movie> movies)
        {
            return new MoviesResponse
            {
                Items = movies.Select(MapToResponse)
            };
        }

        //For Users

        public static User MapToUser(this CreateUserRequest request)
        {
            return new User
            {
                Email = request.Email,
                Admin = request.Admin,
                Trusted_member = request.Trusted_member
            };
        }
    }

    
}
