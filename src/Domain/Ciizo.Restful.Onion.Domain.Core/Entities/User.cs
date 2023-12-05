using Ciizo.Restful.Onion.Domain.Core.Entities.Common;

namespace Ciizo.Restful.Onion.Domain.Core.Entities
{
    public class User : AuditableEntity
    {
        public Guid Id { get; set; }

        public required string Email { get; set; }

        public required string Name { get; set; }
    }
}