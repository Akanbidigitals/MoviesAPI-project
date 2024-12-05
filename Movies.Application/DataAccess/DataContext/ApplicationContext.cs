using Microsoft.EntityFrameworkCore;

namespace Movies.Api.DataAccess.DataContext
{
    public class ApplicationContext:DbContext 
    {
        public ApplicationContext(DbContextOptions options) : base(options) { }
        
        public DbSet<Movie> Movies { get; set; }
        public DbSet<User> Users { get; set; }

            
        
    }
}
