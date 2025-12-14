using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marvel.Application.DTOs.Marvel
{
    /// <summary>
    /// DTO para devolver información de un cómic favorito
    /// </summary>
    public record FavoriteResponse(string ComicId);
}
