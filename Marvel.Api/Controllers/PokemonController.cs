using Marvel.Application.Queries.Marvel;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Marvel.Api.Controllers
{
    /// <summary>
    /// Controlador para consultar información de Pokémon utilizando PokeAPI.
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api/pokemon")]
    public class PokemonController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PokemonController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Obtiene una lista paginada de Pokémon.
        /// </summary>
        /// <param name="offset">Cantidad de registros a omitir.</param>
        /// <param name="limit">Cantidad máxima de Pokémon a retornar.</param>
        /// <returns>Listado paginado de Pokémon.</returns>
        /// <response code="200">Listado obtenido correctamente.</response>
        /// <response code="401">Usuario no autenticado.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetPokemons(
            [FromQuery] int offset = 0,
            [FromQuery] int limit = 20)
        {
            var query = new GetPokemonsQuery(offset, limit);
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        /// <summary>
        /// Obtiene el detalle de un Pokémon por ID o nombre.
        /// </summary>
        /// <param name="idOrName">ID o nombre del Pokémon.</param>
        /// <returns>Detalle del Pokémon.</returns>
        /// <response code="200">Detalle obtenido correctamente.</response>
        /// <response code="404">Pokémon no encontrado.</response>
        /// <response code="401">Usuario no autenticado.</response>
        [HttpGet("{idOrName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetPokemonDetail(string idOrName)
        {
            var query = new GetPokemonDetailQuery(idOrName);
            var result = await _mediator.Send(query);

            return Ok(result);
        }
    }
}
