using Marvel.Application.DTOs.Marvel;
using Marvel.Application.Interfaces;
using Marvel.Application.Queries.Marvel;
using MediatR;

namespace Marvel.Application.Handlers.Pokemon
{
    public class GetPokemonDetailQueryHandler
        : IRequestHandler<GetPokemonDetailQuery, PokemonDetailDto>
    {
        private readonly IPokeApiService _pokeApiService;

        public GetPokemonDetailQueryHandler(IPokeApiService pokeApiService)
        {
            _pokeApiService = pokeApiService;
        }

        public async Task<PokemonDetailDto> Handle(
            GetPokemonDetailQuery request,
            CancellationToken cancellationToken)
        {
            return await _pokeApiService
                .GetPokemonByIdOrNameAsync(request.IdOrName);
        }
    }
}
