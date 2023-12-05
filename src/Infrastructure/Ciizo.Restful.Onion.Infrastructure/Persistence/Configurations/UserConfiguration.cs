using Ciizo.Restful.Onion.Domain.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ciizo.Restful.Onion.Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : AuditableEntityConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("NEWID()");
            builder.Property(x => x.Email)
                .HasMaxLength(256)
                .IsRequired();
            builder.Property(x => x.Name)
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}