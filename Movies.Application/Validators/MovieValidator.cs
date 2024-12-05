using FluentValidation;
using Movies.Application.Repositories;


namespace Movies.Application.Validators
{
    public class MovieValidator: AbstractValidator<Movie>
    {
        private readonly IMovieRepository _ctx;

        public MovieValidator(IMovieRepository ctx)
        {
            _ctx = ctx;

            RuleFor(x=>x.Id).NotEmpty();
            RuleFor(x=>x.Genres).NotEmpty();
            RuleFor(x=>x.Title).NotEmpty();
            RuleFor(x => x.YearOfRelease).LessThanOrEqualTo(DateTime.UtcNow.Year);
            RuleFor(x=>x.Slug).MustAsync(ValidateSlug).WithMessage("This movie already exist in the system");

        }
        private async Task<bool> ValidateSlug(Movie movie,string slug, CancellationToken token= default)
        {
            var existingMovie = await _ctx.GetBySlug(slug);
            if(existingMovie is not null)
            {
                return existingMovie.Id == movie.Id;
            }
            return false;
        }
    }
}
