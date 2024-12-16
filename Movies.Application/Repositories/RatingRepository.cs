using Microsoft.EntityFrameworkCore;
using Movies.Api.DataAccess.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Application.Repositories
{
    public class RatingRepository : IRatingRepository
    {
        private readonly ApplicationContext _dbContext;

        public RatingRepository(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<float?> GetRatingAsync(Guid movieID)
        {
            var ratings = await _dbContext.Movies.Where(x => x.Id == movieID).ToListAsync();
            //Calculate averate ratings
            var averageratings = ratings.Any() ? ratings.Average(r => r.Rating) : null;
            return averageratings;
        }

        public async Task<(float? Rating, int? UserRating)> GetRatingAsync(Guid movieId, Guid userId)
        {
            var movieRatings = await _dbContext.Movies.Where(r => r.Id == movieId).ToListAsync();

            float? averageRatings = movieRatings.Any() ? movieRatings.Average(r => r.Rating) : null;

            // Retrieve the user rating for the movie.
            var user = await _dbContext.Movies.Where(m => m.Id == userId).Include(m => m.UserRating).FirstOrDefaultAsync();
            if (user is null)
            {
                throw new NullReferenceException("User is null");
            }


            return (averageRatings, user.UserRating);

        }

        public async Task<bool> RateMovieAsync(Guid movieId, int rating, Guid userId)
        {
            var movie = await _dbContext.Movies.SingleOrDefaultAsync(x=>x.Id ==movieId);
            var user = await _dbContext.Users.FindAsync(userId);
            if (movie is null && user is null)
            {
                return false;
            }

            

            if (rating < 1 || rating > 5)
            {
                throw new ArgumentOutOfRangeException(nameof(rating), "Rating musbe be betweet 1 and 5");
            }
            //Check if user exist to rate movie
            var existingRating = await _dbContext.Ratings.FirstOrDefaultAsync(r => r.MovieID == movieId && r.UserId == userId);

            if (existingRating != null)
            
            {
                existingRating.Rating = rating;
                _dbContext.Ratings.Update(existingRating);

            }
            
            else
            {
                var newRating = new Ratings
                {
                    MovieID = movieId,
                    UserId = userId,
                    Rating = rating,

                };
                
                
                await _dbContext.Ratings.AddAsync(newRating);


            }
            var result = await _dbContext.SaveChangesAsync();
            return result > 0;
        }


        
    }
  }
