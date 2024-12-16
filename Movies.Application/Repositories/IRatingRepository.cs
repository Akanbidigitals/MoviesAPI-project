using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Application.Repositories
{
    public interface IRatingRepository
    {
        Task<bool> RateMovieAsync(Guid movieId, int rating, Guid userId);

        Task<float?> GetRatingAsync(Guid movieID);
        Task<(float? Rating, int? UserRating)> GetRatingAsync(Guid movieId, Guid userId);
    }

}
