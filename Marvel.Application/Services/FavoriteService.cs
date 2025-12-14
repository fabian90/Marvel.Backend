using Marvel.Application.DTOs;
using Marvel.Application.DTOs.Marvel;
using Marvel.Application.Interfaces;
using Marvel.Domain.Entities;
using Marvel.Domain.Interfaces;
using Marvel.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marvel.Application.Services
{
    public class FavoriteService : IFavoriteService
    {
        private readonly IComicFavoriteRepository _repository;

        public FavoriteService(IComicFavoriteRepository repository)
        {
            _repository = repository;
        }

        public async Task<FavoriteResponse> AddFavoriteAsync(Guid userId, string comicId)
        {
            if (await _repository.ExistsAsync(userId, comicId))
                throw new InvalidOperationException("El cómic ya está en favoritos.");

            var favorite = new ComicFavorite(userId, comicId);
            await _repository.AddAsync(favorite);

            return new FavoriteResponse(favorite.ComicId);
        }

        public async Task RemoveFavoriteAsync(Guid userId, string comicId)
        {
            await _repository.RemoveAsync(userId, comicId);
        }

        public async Task<List<FavoriteResponse>> GetFavoritesByUserAsync(Guid userId)
        {
            var favorites = await _repository.GetByUserIdAsync(userId);
            return favorites.Select(f => new FavoriteResponse(f.ComicId)).ToList();
        }
    }
}
