using Marvel.Domain.Entities;
using Marvel.Domain.Repositories;
using Marvel.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marvel.Infrastructure.Repositories
{
    public class PokemonFavoriteRepository : IPokemonFavoriteRepository
    {
        private readonly ApplicationDbContext _context;

        public PokemonFavoriteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(PokemonFavorite favorite)
        {
            _context.PokemonFavorites.Add(favorite);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(Guid userId, string pokemonId)
        {
            var favorite = await _context.PokemonFavorites
                .FirstOrDefaultAsync(f =>
                    f.UserId == userId &&
                    f.PokemonId == pokemonId);

            if (favorite != null)
            {
                _context.PokemonFavorites.Remove(favorite);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(Guid userId, string pokemonId)
        {
            return await _context.PokemonFavorites
                .AnyAsync(f =>
                    f.UserId == userId &&
                    f.PokemonId == pokemonId);
        }

        public async Task<List<PokemonFavorite>> GetByUserIdAsync(Guid userId)
        {
            return await _context.PokemonFavorites
                .Where(f => f.UserId == userId)
                .ToListAsync();
        }
    }
}