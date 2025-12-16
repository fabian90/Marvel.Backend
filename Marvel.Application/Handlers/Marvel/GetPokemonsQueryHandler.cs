using Marvel.Application.DTOs.Marvel;
using Marvel.Application.Interfaces;
using Marvel.Application.Queries.Marvel;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Marvel.Application.Handlers.Marvel
{
    public class GetPokemonsQueryHandler
        : IRequestHandler<GetPokemonsQuery, PokemonListResponseDto>
    {
        private readonly IPokeApiService _pokeApiService;
        private readonly IMemoryCache _cache;

        public GetPokemonsQueryHandler(
            IPokeApiService pokeApiService,
            IMemoryCache cache)
        {
            _pokeApiService = pokeApiService;
            _cache = cache;
        }

        public async Task<PokemonListResponseDto> Handle(
            GetPokemonsQuery request,
            CancellationToken cancellationToken)
        {
            var cacheKey = $"pokemons_{request.Offset}_{request.Limit}";

            if (_cache.TryGetValue(cacheKey, out PokemonListResponseDto cached))
            {
                return cached;
            }

            var result = await _pokeApiService.GetPokemonsAsync(
                request.Offset,
                request.Limit);

            _cache.Set(
                cacheKey,
                result,
                TimeSpan.FromMinutes(5)
            );

            return result;
        }
    }
}
