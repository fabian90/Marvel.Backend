using Marvel.Application.DTOs;
using Marvel.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System;
using System.Threading.Tasks;
using Marvel.Application.DTOs.Marvel;
using Marvel.Application.Interfaces;

namespace Marvel.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class FavoritesController : ControllerBase
    {
        private readonly IFavoriteService _favoriteService;

        public FavoritesController(IFavoriteService favoriteService)
        {
            _favoriteService = favoriteService;
        }

        private Guid GetUserId() =>
            Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        /// <summary>
        /// Agrega un cómic a la lista de favoritos del usuario.
        /// </summary>
        /// <param name="request">DTO con el ID del cómic a agregar.</param>
        /// <returns>Favorito agregado.</returns>
        /// <response code="200">Devuelve el cómic agregado.</response>
        /// <response code="400">Si el ComicId está vacío o no válido.</response>
        [HttpPost]
        [ProducesResponseType(typeof(FavoriteResponse), 200)]
        [ProducesResponseType(typeof(ProblemDetails), 400)]
        public async Task<IActionResult> AddFavorite([FromBody] AddFavoriteRequest request)
        {
            var result = await _favoriteService.AddFavoriteAsync(GetUserId(), request.PokemonId);
            return Ok(result);
        }

        /// <summary>
        /// Elimina un cómic de la lista de favoritos del usuario.
        /// </summary>
        /// <param name="comicId">ID del cómic a eliminar.</param>
        /// <response code="204">Cómic eliminado correctamente.</response>
        [HttpDelete("{comicId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(ProblemDetails), 400)]
        public async Task<IActionResult> RemoveFavorite(string comicId)
        {
            await _favoriteService.RemoveFavoriteAsync(GetUserId(), comicId);
            return NoContent();
        }

        /// <summary>
        /// Obtiene la lista de cómics favoritos del usuario.
        /// </summary>
        /// <returns>Lista de favoritos.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(FavoriteResponse[]), 200)]
        public async Task<IActionResult> GetFavorites()
        {
            var result = await _favoriteService.GetFavoritesByUserAsync(GetUserId());
            return Ok(result);
        }
    }
}
