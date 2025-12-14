using Marvel.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Marvel.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<ComicFavorite> ComicFavorites { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración User
            modelBuilder.Entity<User>(builder =>
            {
                builder.HasKey(u => u.Id);
                builder.HasIndex(u => u.Email).IsUnique();
                builder.HasMany(u => u.Favorites)
                       .WithOne()
                       .HasForeignKey(f => f.UserId)
                       .OnDelete(DeleteBehavior.Cascade);
            });

            // Configuración ComicFavorite
            modelBuilder.Entity<ComicFavorite>(builder =>
            {
                builder.HasKey(f => f.Id);
                builder.Property(f => f.UserId).IsRequired();
                builder.Property(f => f.ComicId).IsRequired();
                builder.HasIndex(f => new { f.UserId, f.ComicId }).IsUnique();
            });
        }
    }
}