namespace Marvel.Application.DTOs.Marvel
{
    public class MarvelApiResponse<T>
    {
        public int Code { get; set; }
        public string Status { get; set; }
        public MarvelData<T> Data { get; set; }
    }

    public class MarvelData<T>
    {
        public int Offset { get; set; }
        public int Limit { get; set; }
        public int Total { get; set; }
        public int Count { get; set; }
        public IEnumerable<T> Results { get; set; }
    }

    public class ComicDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ThumbnailDto Thumbnail { get; set; }
    }

    public class ThumbnailDto
    {
        public string Path { get; set; }
        public string Extension { get; set; }
    }
}
