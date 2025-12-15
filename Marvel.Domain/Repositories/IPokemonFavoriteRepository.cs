using Marvel.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marvel.Domain.Repositories
{
    public interface IPokemonFavoriteRepository
    {
        Task AddAsync(PokemonFavorite favorite);
        Task RemoveAsync(Guid userId, string pokemonId);
        Task<List<PokemonFavorite>> GetByUserIdAsync(Guid userId);
        Task<bool> ExistsAsync(Guid userId, string pokemonId);
    }
}
