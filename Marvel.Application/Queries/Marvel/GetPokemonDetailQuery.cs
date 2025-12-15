using Marvel.Application.DTOs.Marvel;
using MediatR;

namespace Marvel.Application.Queries.Marvel
{
    public record GetPokemonDetailQuery(string IdOrName)
        : IRequest<PokemonDetailDto>;
}
