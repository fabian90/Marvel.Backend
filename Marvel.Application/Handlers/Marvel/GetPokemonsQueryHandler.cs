using Marvel.Application.DTOs.Marvel;
using Marvel.Application.Interfaces;
using Marvel.Application.Queries.Marvel;
using MediatR;

namespace Marvel.Application.Handlers.Marvel
{
    public class GetPokemonsQueryHandler
        : IRequestHandler<GetPokemonsQuery, PokemonListResponseDto>
    {
        private readonly IPokeApiService _pokeApiService;

        public GetPokemonsQueryHandler(IPokeApiService pokeApiService)
        {
            _pokeApiService = pokeApiService;
        }

        public async Task<PokemonListResponseDto> Handle(
            GetPokemonsQuery request,
            CancellationToken cancellationToken)
        {
            return await _pokeApiService.GetPokemonsAsync(
                request.Offset,
                request.Limit);
        }
    }
}
