using Ciizo.CleanPattern.Domain.Core.Entities;
using Ciizo.CleanPattern.Domain.Core.Repository;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Ciizo.CleanPattern.Infrastructure.Persistence
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