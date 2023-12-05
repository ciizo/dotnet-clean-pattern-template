using Ciizo.Restful.Onion.Domain.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ciizo.Restful.Onion.Domain.Core.Repository
{
    public interface IApplicationDbContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        DbSet<User> Users { get; }
    }
}