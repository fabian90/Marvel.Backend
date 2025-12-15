using Marvel.Application.DTOs;
using Marvel.Application.DTOs.Marvel;
using Marvel.Application.Interfaces;
using Marvel.Domain.Entities;
using Marvel.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marvel.Application.Services
{
    public class FavoriteService : IFavoriteService
    {
        private readonly IPokemonFavoriteRepository _repository;

        public FavoriteService(IPokemonFavoriteRepository repository)
        {
            _repository = repository;
        }

        public async Task<FavoriteResponse> AddFavoriteAsync(Guid userId, string pokemonId)
        {
            if (await _repository.ExistsAsync(userId, pokemonId))
                throw new InvalidOperationException("El Pokémon ya está en favoritos.");

            var favorite = new PokemonFavorite(userId, pokemonId);
            await _repository.AddAsync(favorite);

            return new FavoriteResponse(favorite.PokemonId);
        }

        public async Task RemoveFavoriteAsync(Guid userId, string pokemonId)
        {
            await _repository.RemoveAsync(userId, pokemonId);
        }

        public async Task<List<FavoriteResponse>> GetFavoritesByUserAsync(Guid userId)
        {
            var favorites = await _repository.GetByUserIdAsync(userId);

            return favorites
                .Select(f => new FavoriteResponse(f.PokemonId))
                .ToList();
        }
    }
}
