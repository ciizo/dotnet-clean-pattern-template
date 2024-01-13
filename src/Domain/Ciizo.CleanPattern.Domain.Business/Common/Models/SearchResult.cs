namespace Ciizo.CleanPattern.Domain.Business.Common.Models
{
    public record SearchResult<T>
    {
        public IEnumerable<T> Data { get; set; } = Enumerable.Empty<T>();
        public int TotalCount { get; set; }
    }
}