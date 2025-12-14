using Marvel.Application.DTOs.Marvel;
using MediatR;

namespace Marvel.Application.Queries.Marvel
{
    public record GetComicDetailQuery(string Id) : IRequest<MarvelApiResponse<ComicDto>>;
}
