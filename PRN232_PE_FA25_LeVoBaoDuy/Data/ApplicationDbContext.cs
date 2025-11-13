using Microsoft.EntityFrameworkCore;
using PRN232_PE_FA25_LeVoBaoDuy.Models;

namespace PRN232_PE_FA25_LeVoBaoDuy.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Movie> Movies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed sample movies
            modelBuilder.Entity<Movie>().HasData(
                new Movie { Id = 1, Title = "The Matrix", Genre = "Sci-Fi", Rating = 5, PosterImage = "https://picsum.photos/seed/matrix/400/600", CreatedAt = DateTime.UtcNow },
                new Movie { Id = 2, Title = "Inception", Genre = "Sci-Fi", Rating = 5, PosterImage = "https://picsum.photos/seed/inception/400/600", CreatedAt = DateTime.UtcNow },
                new Movie { Id = 3, Title = "The Dark Knight", Genre = "Action", Rating = 5, PosterImage = "https://picsum.photos/seed/darkknight/400/600", CreatedAt = DateTime.UtcNow }
            );
        }
    }
}
