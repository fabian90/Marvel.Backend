using Marvel.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marvel.Domain.Repositories
{
    public interface IComicFavoriteRepository
    {
        Task AddAsync(ComicFavorite favorite);
        Task RemoveAsync(Guid userId, string comicId);
        Task<List<ComicFavorite>> GetByUserIdAsync(Guid userId);
        Task<bool> ExistsAsync(Guid userId, string comicId);
    }
}
