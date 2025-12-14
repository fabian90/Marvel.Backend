using Marvel.Application.DTOs.Marvel;
using MediatR;

namespace Marvel.Application.Queries.Marvel
{
    public record GetComicsQuery(int Offset, int Limit) : IRequest<MarvelApiResponse<ComicDto>>;
}
