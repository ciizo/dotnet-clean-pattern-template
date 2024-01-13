using Ciizo.CleanPattern.Domain.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ciizo.CleanPattern.Domain.Core.Repository
{
    public interface IApplicationDbContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        DbSet<User> Users { get; }
    }
}