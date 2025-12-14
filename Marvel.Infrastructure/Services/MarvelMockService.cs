using Marvel.Application.DTOs.Marvel;
using Marvel.Application.Interfaces;

namespace Marvel.Infrastructure.Services
{
    public class MarvelMockService : IMarvelMockService
    {
        private readonly List<ComicDto> _mockComics;
        private readonly Random _random;

        public MarvelMockService()
        {
            _random = new Random();
            _mockComics = GenerateMockComics(100);
        }

        private List<ComicDto> GenerateMockComics(int count)
        {
            var comics = new List<ComicDto>();
            for (int i = 1; i <= count; i++)
            {
                comics.Add(new ComicDto
                {
                    Id = (1000000 + i).ToString(),
                    Title = $"Comic Title #{i}",
                    Description = $"This is the description for Comic #{i}. It tells a thrilling story of heroes and villains.",
                    Thumbnail = new ThumbnailDto
                    {
                        Path = $"https://example.com/comics/{1000000 + i}",
                        Extension = "jpg"
                    }
                });
            }
            return comics;
        }

        public async Task<MarvelApiResponse<ComicDto>> GetComicsAsync(int offset, int limit)
        {
            await SimulateLatency();

            // Simulate 401 Unauthorized for specific offset (example)
            if (offset == 1000)
            {
                return new MarvelApiResponse<ComicDto>
                {
                    Code = 401,
                    Status = "Unauthorized",
                    Data = null
                };
            }

            var paginatedComics = _mockComics.Skip(offset).Take(limit).ToList();

            return new MarvelApiResponse<ComicDto>
            {
                Code = 200,
                Status = "Ok",
                Data = new MarvelData<ComicDto>
                {
                    Offset = offset,
                    Limit = limit,
                    Total = _mockComics.Count,
                    Count = paginatedComics.Count,
                    Results = paginatedComics
                }
            };
        }

        public async Task<MarvelApiResponse<ComicDto>> GetComicByIdAsync(string id)
        {
            await SimulateLatency();

            // Simulate 404 Not Found for specific ID (example)
            if (id == "9999999") 
            {
                return new MarvelApiResponse<ComicDto>
                {
                    Code = 404,
                    Status = "Not Found",
                    Data = null
                };
            }

            var comic = _mockComics.FirstOrDefault(c => c.Id == id);

            if (comic == null)
            {
                return new MarvelApiResponse<ComicDto>
                {
                    Code = 404,
                    Status = "Not Found",
                    Data = null
                };
            }

            return new MarvelApiResponse<ComicDto>
            {
                Code = 200,
                Status = "Ok",
                Data = new MarvelData<ComicDto>
                {
                    Offset = 0,
                    Limit = 1,
                    Total = 1,
                    Count = 1,
                    Results = new List<ComicDto> { comic }
                }
            };
        }

        private async Task SimulateLatency()
        {
            // Simulate network latency between 50ms and 500ms
            var delay = _random.Next(50, 500);
            await Task.Delay(delay);
        }
    }
}
