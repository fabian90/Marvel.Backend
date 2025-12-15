using Marvel.Application.DTOs.Marvel;
using MediatR;

namespace Marvel.Application.Queries.Marvel
{
    public record GetPokemonsQuery(int Offset, int Limit)
    : IRequest<PokemonListResponseDto>;
}
