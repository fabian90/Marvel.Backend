using Marvel.Application.Queries.Marvel;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Marvel.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class MarvelController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MarvelController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Recupera una lista de cómics de Marvel.
        /// </summary>
        /// <param name="offset">El número de resultados a omitir.</param>
        /// <param name="limit">El número máximo de resultados a devolver.</param>
        /// <returns>Una lista de cómics de Marvel.</returns>
        /// <response code="200">Devuelve una lista de cómics de Marvel.</response>
        /// <response code="401">Si el usuario no está autenticado.</response>
        [HttpGet]
        [ProducesResponseType(typeof(object), 200)] // Usando object ya que el DTO real es complejo y anidado
        [ProducesResponseType(typeof(ProblemDetails), 401)]
        public async Task<IActionResult> GetComics([FromQuery] int offset = 0, [FromQuery] int limit = 20)
        {
            var query = new GetComicsQuery(offset, limit);
            var result = await _mediator.Send(query);

            if (result.Code == 401)
            {
                return Unauthorized(result);
            }

            return Ok(result);
        }

        /// <summary>
        /// Recupera los detalles de un cómic específico de Marvel por su ID.
        /// </summary>
        /// <param name="id">El ID del cómic a recuperar.</param>
        /// <returns>Detalles del cómic específico de Marvel.</returns>
        /// <response code="200">Devuelve los detalles del cómic de Marvel.</response>
        /// <response code="404">Si no se encuentra el cómic con el ID proporcionado.</response>
        /// <response code="401">Si el usuario no está autenticado.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(object), 200)] // Usando object ya que el DTO real es complejo y anidado
        [ProducesResponseType(typeof(ProblemDetails), 404)]
        [ProducesResponseType(typeof(ProblemDetails), 401)]
        public async Task<IActionResult> GetComicDetail(string id)
        {
            var query = new GetComicDetailQuery(id);
            var result = await _mediator.Send(query);

            if (result.Code == 404)
            {
                return NotFound(result);
            }
            if (result.Code == 401)
            {
                return Unauthorized(result);
            }

            return Ok(result);
        }
    }
}
