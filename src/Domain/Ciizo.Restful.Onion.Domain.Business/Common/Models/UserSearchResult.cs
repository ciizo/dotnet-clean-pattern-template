namespace Ciizo.Restful.Onion.Domain.Business.Common.Models
{
    public record UserSearchResult<T>
    {
        public IEnumerable<T> Data { get; set; } = Enumerable.Empty<T>();
        public int TotalCount { get; set; }
    }
}