using Microsoft.EntityFrameworkCore;

namespace Movies.Api.DataAccess.DataContext
{
    public class ApplicationContext:DbContext 
    {
        public ApplicationContext(DbContextOptions options) : base(options) { }
        
        public DbSet<Movie> Movies { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet <Ratings> Ratings { get; set; }


        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    // Configure relationships if not using Fluent API in entities
        //    modelBuilder.Entity<Ratings>()
        //        .HasOne(r => r.Movie)
        //        .WithMany(m => m.Ratings)
        //        .HasForeignKey(r => r.MovieID);

        //    modelBuilder.Entity<Ratings>()
        //        .HasOne(r => r.User)
        //        .WithMany()
        //        .HasForeignKey(r => r.UserId);
        //}
    }
}
