using Marvel.Application.DTOs.Marvel;
using Marvel.Application.Interfaces;
using Marvel.Application.Queries.Marvel;
using MediatR;

namespace Marvel.Application.Handlers.Marvel
{
    public class GetComicsQueryHandler : IRequestHandler<GetComicsQuery, MarvelApiResponse<ComicDto>>
    {
        private readonly IMarvelMockService _marvelMockService;

        public GetComicsQueryHandler(IMarvelMockService marvelMockService)
        {
            _marvelMockService = marvelMockService;
        }

        public async Task<MarvelApiResponse<ComicDto>> Handle(GetComicsQuery request, CancellationToken cancellationToken)
        {
            return await _marvelMockService.GetComicsAsync(request.Offset, request.Limit);
        }
    }
}
