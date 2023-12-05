using Ciizo.Restful.Onion.Domain.Core.Entities;
using Ciizo.Restful.Onion.Domain.Core.Repository;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Ciizo.Restful.Onion.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users => Set<User>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}