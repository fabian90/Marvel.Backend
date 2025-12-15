using Marvel.Application.DTOs.Marvel;
using Marvel.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marvel.Application.Interfaces
{
    public interface IFavoriteService
    {
        /// <summary>
        /// Agrega un Pokémon a la lista de favoritos del usuario.
        /// </summary>
        /// <param name="userId">Identificador del usuario.</param>
        /// <param name="pokemonId">ID o nombre del Pokémon.</param>
        /// <returns>Información del favorito agregado.</returns>
        Task<FavoriteResponse> AddFavoriteAsync(Guid userId, string pokemonId);

        /// <summary>
        /// Elimina un Pokémon de la lista de favoritos del usuario.
        /// </summary>
        /// <param name="userId">Identificador del usuario.</param>
        /// <param name="pokemonId">ID o nombre del Pokémon.</param>
        Task RemoveFavoriteAsync(Guid userId, string pokemonId);

        /// <summary>
        /// Obtiene la lista de Pokémon favoritos de un usuario.
        /// </summary>
        /// <param name="userId">Identificador del usuario.</param>
        /// <returns>Lista de favoritos.</returns>
        Task<List<FavoriteResponse>> GetFavoritesByUserAsync(Guid userId);
    }
}
