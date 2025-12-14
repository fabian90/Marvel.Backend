using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marvel.Application.DTOs.Marvel
{
    /// <summary>
    /// DTO para solicitar eliminar un cómic de favoritos
    /// </summary>
    public record RemoveFavoriteRequest
    {
        public string ComicId { get; init; } = string.Empty;
    }
}
