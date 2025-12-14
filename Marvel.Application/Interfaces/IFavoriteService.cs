using Marvel.Application.DTOs.Marvel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marvel.Application.Interfaces
{
    public interface IFavoriteService
    {
        Task<FavoriteResponse> AddFavoriteAsync(Guid userId, string comicId);
        Task RemoveFavoriteAsync(Guid userId, string comicId);
        Task<List<FavoriteResponse>> GetFavoritesByUserAsync(Guid userId);
    }
}
