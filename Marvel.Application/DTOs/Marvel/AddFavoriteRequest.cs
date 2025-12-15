using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marvel.Application.DTOs.Marvel
{
    /// <summary>
    /// DTO para solicitar agregar un cómic a favoritos
    /// </summary>
    public record AddFavoriteRequest
    {
        public string PokemonId { get; init; } = string.Empty;
    }
}
