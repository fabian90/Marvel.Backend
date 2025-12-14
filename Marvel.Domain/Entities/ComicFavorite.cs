using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marvel.Domain.Entities
{
    public class ComicFavorite
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public string ComicId { get; private set; }

        private ComicFavorite() { } // Para EF Core

        public ComicFavorite(Guid userId, string comicId)
        {
            if (string.IsNullOrWhiteSpace(comicId))
                throw new ArgumentException("ComicId no puede estar vacío.", nameof(comicId));

            Id = Guid.NewGuid();
            UserId = userId;
            ComicId = comicId;
        }
    }
}
