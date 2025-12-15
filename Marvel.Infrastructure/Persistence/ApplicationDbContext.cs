using Marvel.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Marvel.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<PokemonFavorite> PokemonFavorites => Set<PokemonFavorite>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // =======================
            // User
            // =======================
            modelBuilder.Entity<User>(builder =>
            {
                builder.HasKey(u => u.Id);

                builder.Property(u => u.Email)
                       .IsRequired()
                       .HasMaxLength(150);

                builder.HasIndex(u => u.Email)
                       .IsUnique();

                builder.HasMany(u => u.Favorites)
                       .WithOne()
                       .HasForeignKey(f => f.UserId)
                       .OnDelete(DeleteBehavior.Cascade);
            });

            // =======================
            // PokemonFavorite
            // =======================
            modelBuilder.Entity<PokemonFavorite>(builder =>
            {
                // Clave compuesta (UserId + PokemonId)
                builder.HasKey(f => new { f.UserId, f.PokemonId });

                builder.Property(f => f.UserId)
                       .IsRequired()
                       .HasMaxLength(50);
            });
        }
    }
}
