using Microsoft.Azure.Documents;
using Microsoft.EntityFrameworkCore;
using MovieWebsite.Models;
using MovieWebsite.Models; // Thay thế YourNamespace bằng namespace chính xác

namespace MovieWebsite.Data
{
    public class MovieDbContext : DbContext
    {
        public DbSet<Movies> Movies { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<MoviesActors> MovieActors { get; set; }
		public DbSet<Episode> Episodes { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MoviesActors>()
                .HasKey(ma => new { ma.MoviesId, ma.ActorsId });

            modelBuilder.Entity<MoviesActors>()
                .HasOne(ma => ma.Movie)
                .WithMany(m => m.MoviesActors)
                .HasForeignKey(ma => ma.MoviesId);

            modelBuilder.Entity<MoviesActors>()
                .HasOne(ma => ma.Actor)
                .WithMany(a => a.MoviesActors)
                .HasForeignKey(ma => ma.ActorsId);
        }
    }
}
