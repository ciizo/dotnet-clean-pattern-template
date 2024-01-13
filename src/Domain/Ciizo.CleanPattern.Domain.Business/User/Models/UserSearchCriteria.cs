namespace Ciizo.CleanPattern.Domain.Business.User.Models
{
    public record UserSearchCriteria
    {
        public string Name { get; set; } = null!;
    }
}