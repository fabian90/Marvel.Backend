using Marvel.Application.DTOs.Auth;
using Marvel.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Marvel.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        /// <summary>
        /// Registra un nuevo usuario en el sistema y genera un token de autenticación (JWT).
        /// </summary>
        /// <param name="request">Datos requeridos para el registro del usuario.</param>
        /// <returns>Información de autenticación con el token JWT y su fecha de expiración.</returns>
        /// <response code="200">Usuario registrado exitosamente.</response>
        /// <response code="400">Los datos enviados son inválidos o el usuario ya existe.</response>
        [HttpPost("register")]
        [ProducesResponseType(typeof(AuthResponseDto), 200)]
        [ProducesResponseType(typeof(ProblemDetails), 400)]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
        {
            var result = await _authenticationService.RegisterAsync(request);
            return Ok(result);
        }

        /// <summary>
        /// Autentica a un usuario existente y genera un token de acceso (JWT).
        /// </summary>
        /// <param name="request">Credenciales de inicio de sesión del usuario.</param>
        /// <returns>Información de autenticación con el token JWT y su fecha de expiración.</returns>
        /// <response code="200">Autenticación exitosa.</response>
        /// <response code="401">Las credenciales proporcionadas son incorrectas.</response>
        [HttpPost("login")]
        [ProducesResponseType(typeof(AuthResponseDto), 200)]
        [ProducesResponseType(typeof(ProblemDetails), 401)]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            var result = await _authenticationService.LoginAsync(request);
            return Ok(result);
        }
    }
}
