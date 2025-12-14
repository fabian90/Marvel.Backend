using Marvel.Application.DTOs.Auth;
using System.Threading.Tasks;

namespace Marvel.Application.Interfaces
{
    /// <summary>
    /// Define los métodos para la autenticación de usuarios, incluyendo el registro y el inicio de sesión.
    /// </summary>
    public interface IAuthenticationService
    {
        /// <summary>
        /// Registra un nuevo usuario en el sistema y devuelve un token de autenticación.
        /// </summary>
        /// <param name="request">Datos de registro del nuevo usuario.</param>
        /// <returns>Un objeto <see cref="AuthResponseDto"/> que contiene el token y la expiración del token.</returns>
        Task<AuthResponseDto> RegisterAsync(RegisterRequestDto request);

        /// <summary>
        /// Inicia sesión de un usuario existente y devuelve un token de autenticación.
        /// </summary>
        /// <param name="request">Credenciales de inicio de sesión del usuario.</param>
        /// <returns>Un objeto <see cref="AuthResponseDto"/> que contiene el token y la expiración del token.</returns>
        Task<AuthResponseDto> LoginAsync(LoginRequestDto request);
    }
}
