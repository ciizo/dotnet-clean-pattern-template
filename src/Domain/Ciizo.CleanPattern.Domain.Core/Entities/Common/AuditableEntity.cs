namespace Ciizo.CleanPattern.Domain.Core.Entities.Common
{
    public class AuditableEntity
    {
        public DateTime Created { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? Updated { get; set; }

        public string? UpdatedBy { get; set; }
    }
}