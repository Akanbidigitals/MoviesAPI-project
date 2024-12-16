using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Application.Services
{
    public interface IMovieService
    {
        Task<bool> CreateAsync(Movie movie);

        Task<Movie?> GetByIdAsync(Guid id,Guid? userid = default);

        Task<IEnumerable<Movie>> GetAllAsync(Guid? userid = default);
        Task<bool> DeleteByIdAsync(Guid id);
        Task<Movie?> GetBySlug(string slug, Guid? userid = default);
        Task<Movie?> UpdateAsync(Movie movie, Guid? userid = default);
        
    }
}
