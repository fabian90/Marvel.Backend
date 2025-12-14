using Marvel.Domain.Entities;

namespace Marvel.Application.Interfaces
{
    /// <summary>
    /// Define el contrato para la generación de tokens JWT utilizados en la autenticación.
    /// </summary>
    public interface IJwtProvider
    {
        /// <summary>
        /// Genera un token JWT para un usuario autenticado.
        /// </summary>
        /// <param name="user">Entidad del usuario para el cual se generará el token.</param>
        /// <returns>Token JWT en formato de cadena.</returns>
        string Generate(User user);
    }
}

