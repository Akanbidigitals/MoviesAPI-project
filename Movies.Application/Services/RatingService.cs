using Movies.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Application.Services
{
    public class RatingService : IRatingService
    {
        private readonly IRatingRepository _ratingsRepository;  
        private readonly IMovieRepository _movieRepository;

        public RatingService(IMovieRepository movieRepository,IRatingRepository ratingRepository)
        {
            _movieRepository = movieRepository;
            _ratingsRepository = ratingRepository;
        }

        public async Task<bool> RateMovieAsync(Guid movieId, int rating, Guid userId)
        {
          
            
            return await _ratingsRepository.RateMovieAsync(movieId, rating, userId);
        }
    }
}
