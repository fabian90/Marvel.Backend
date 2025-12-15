using Marvel.Application.DTOs.Marvel;

namespace Marvel.Application.Interfaces
{
    public interface IPokeApiService
    {
        Task<PokemonListResponseDto> GetPokemonsAsync(int offset, int limit);
        Task<PokemonDetailDto?> GetPokemonByIdOrNameAsync(string idOrName);
    }
}
