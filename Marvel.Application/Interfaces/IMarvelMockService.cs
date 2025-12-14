using Marvel.Application.DTOs.Marvel;

namespace Marvel.Application.Interfaces
{
    /// <summary>
    /// Define las operaciones de acceso a datos de prueba (mock) para la API de Marvel.
    /// </summary>
    public interface IMarvelMockService
    {
        /// <summary>
        /// Obtiene una lista paginada de cómics de Marvel.
        /// </summary>
        /// <param name="offset">Número de elementos a omitir al inicio de la lista.</param>
        /// <param name="limit">Cantidad máxima de cómics a retornar.</param>
        /// <returns>Una respuesta de la API con la lista de cómics.</returns>
        Task<MarvelApiResponse<ComicDto>> GetComicsAsync(int offset, int limit);

        /// <summary>
        /// Obtiene un cómic específico por su identificador.
        /// </summary>
        /// <param name="id">Identificador del cómic.</param>
        /// <returns>Una respuesta de la API con los datos del cómic.</returns>
        Task<MarvelApiResponse<ComicDto>> GetComicByIdAsync(string id);
    }
}
