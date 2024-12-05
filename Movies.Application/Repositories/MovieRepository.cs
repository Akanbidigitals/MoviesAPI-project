
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Movies.Api.DataAccess.DataContext;


namespace Movies.Application.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly ApplicationContext _ctx;
        
      public MovieRepository(ApplicationContext ctx)
        {
            _ctx = ctx;
           
        }

     
        public async Task<bool> CreateAsync(Movie movie)
        {
            try
            {

                var check = await _ctx.Movies.AddAsync(movie);
                   
                var res = await _ctx.SaveChangesAsync();
                if(res > 0)
                {
                    return true;
                }
                return false;

            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteByIdAsync(Guid id)
        {

            var check = await _ctx.Movies.FirstOrDefaultAsync(x => x.Id == id);
            if(check is null)
            {
                throw new Exception("Item does not exist");
            }
             _ctx.Movies.Remove(check);
            var res =  _ctx.SaveChanges();
            if(res > 0)
            {
                return true;
            }
            return false;
            
        }

        public async Task<bool> ExistByIdAsync(Guid id, Guid userid = default)
        {
            var Exist = await _ctx.Movies.AnyAsync(x => x.Id == id);
            if (Exist)
            {
                return true;
            }
            return false;

        }

        public async Task<IEnumerable<Movie>> GetAllAsync(Guid userid = default)
        {
            var allmovies = await _ctx.Movies.ToListAsync();
            return allmovies;
        }

        public async Task<Movie?> GetByIdAsync(Guid id, Guid userid = default)
        {
            var getbyId = await _ctx.Movies.FirstOrDefaultAsync(x => x.Id == id);
            if(getbyId is null)
            {
                return null;
            }
            return getbyId;
        }

        public async Task<Movie?> GetBySlug(string slug, Guid userid = default)
        {
            var getbySlug = await _ctx.Movies.FirstOrDefaultAsync(x => x.Slug == slug);
            if(getbySlug is null)
            {
                return null;
            }
            return getbySlug;
        }

        public async Task<bool> UpdateAsync(Movie movie, Guid userid = default)
        {
            var updateMovie = await _ctx.Movies.FirstOrDefaultAsync(x => x.Id == movie.Id);
            if(updateMovie is null)
            {
                return false;
            }
            updateMovie.Id = updateMovie.Id;
            updateMovie.Title = movie.Title;
            updateMovie.YearOfRelease = movie.YearOfRelease;
            updateMovie.Genres =  movie.Genres;

            _ctx.Movies.Update(updateMovie);
            var res = await _ctx.SaveChangesAsync();
            if(res > 0)
            {
                return true;
            }
            return false;
        }
    }
}
