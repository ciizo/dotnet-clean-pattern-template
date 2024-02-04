namespace Ciizo.CleanPattern.Domain.Business.UserCqrs.Models
{
    public record UserCqrsSearchCriteria
    {
        public string Name { get; set; } = null!;
    }
}