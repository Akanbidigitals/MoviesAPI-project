using FluentValidation;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Movies.Application.Repositories;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Application.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movierepo;
        private readonly IValidator<Movie> _validator;
        private readonly Jwt _setup;
        public MovieService(IMovieRepository movierepo,IValidator<Movie> validator,IOptions<Jwt> setup)
        {
            _movierepo = movierepo;
            _validator = validator;
            _setup = setup.Value;
        }
        public async Task<bool> CreateAsync(Movie movie)
        {
           // await _validator.ValidateAndThrowAsync(movie);
            return await _movierepo.CreateAsync(movie);
        }

        public Task<bool> DeleteByIdAsync(Guid id)
        {
            return (_movierepo.DeleteByIdAsync(id));
        }

        public Task<IEnumerable<Movie>> GetAllAsync(Guid userid = default)
        {
            return _movierepo.GetAllAsync(userid);
        }

        public Task<Movie?> GetByIdAsync(Guid id, Guid userid = default)
        {
            return _movierepo.GetByIdAsync(id,userid);
        }

        public Task<Movie?> GetBySlug(string slug, Guid userid = default    )
        {
            return _movierepo.GetBySlug(slug,userid);
        }

      

        public async Task<Movie?> UpdateAsync(Movie movie, Guid userid = default)
        {
          //  await _validator.ValidateAndThrowAsync(movie);
            var movieExist = await _movierepo.ExistByIdAsync(movie.Id);
            if (!movieExist)
            {
                return null;
            }
            await _movierepo.UpdateAsync(movie,userid);
            return movie;
        }
    }
}
