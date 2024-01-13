using Ciizo.CleanPattern.Domain.Core.Entities.Common;

namespace Ciizo.CleanPattern.Domain.Core.Entities
{
    public class User : AuditableEntity
    {
        public Guid Id { get; set; }

        public required string Email { get; set; }

        public required string Name { get; set; }
    }
}