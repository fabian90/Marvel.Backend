using Marvel.Domain.Entities;
using Marvel.Domain.Interfaces;
using Marvel.Domain.Repositories;
using Marvel.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Marvel.Infrastructure.Repositories
{
    public class ComicFavoriteRepository : IComicFavoriteRepository
    {
        private readonly ApplicationDbContext _context;

        public ComicFavoriteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(ComicFavorite favorite)
        {
            _context.ComicFavorites.Add(favorite);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(Guid userId, string comicId)
        {
            var favorite = await _context.ComicFavorites
                .FirstOrDefaultAsync(f => f.UserId == userId && f.ComicId == comicId);

            if (favorite != null)
            {
                _context.ComicFavorites.Remove(favorite);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<ComicFavorite>> GetByUserIdAsync(Guid userId)
        {
            return await _context.ComicFavorites
                .Where(f => f.UserId == userId)
                .ToListAsync();
        }

        public async Task<bool> ExistsAsync(Guid userId, string comicId)
        {
            return await _context.ComicFavorites
                .AnyAsync(f => f.UserId == userId && f.ComicId == comicId);
        }
    }
}
