using Ciizo.Restful.Onion.Domain.Core.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ciizo.Restful.Onion.Infrastructure.Persistence.Configurations
{
    public class AuditableEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
        where TEntity : AuditableEntity
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.Property(x => x.CreatedBy)
                .HasMaxLength(100);
            builder.Property(x => x.UpdatedBy)
                .HasMaxLength(100);
        }
    }
}