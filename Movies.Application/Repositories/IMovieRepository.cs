global using  Movies.Application.Model;


namespace Movies.Application.Repositories
{
    public interface IMovieRepository
    {
        Task<bool> CreateAsync(Movie movie);

        Task<Movie?> GetByIdAsync(Guid id, Guid? userid = default);
        Task<Movie?> GetBySlug(string slug, Guid? userid = default);

        Task<IEnumerable<Movie>> GetAllAsync(Guid? userid );
        Task<bool> DeleteByIdAsync(Guid id);
        Task<bool> UpdateAsync(Movie movie, Guid? userid = default);

        Task <bool> ExistByIdAsync(Guid id, Guid? userid = default);
    }
}
