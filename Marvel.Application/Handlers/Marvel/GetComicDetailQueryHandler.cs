using Marvel.Application.DTOs.Marvel;
using Marvel.Application.Interfaces;
using Marvel.Application.Queries.Marvel;
using MediatR;

namespace Marvel.Application.Handlers.Marvel
{
    public class GetComicDetailQueryHandler : IRequestHandler<GetComicDetailQuery, MarvelApiResponse<ComicDto>>
    {
        private readonly IMarvelMockService _marvelMockService;

        public GetComicDetailQueryHandler(IMarvelMockService marvelMockService)
        {
            _marvelMockService = marvelMockService;
        }

        public async Task<MarvelApiResponse<ComicDto>> Handle(GetComicDetailQuery request, CancellationToken cancellationToken)
        {
            return await _marvelMockService.GetComicByIdAsync(request.Id);
        }
    }
}
