using System.Linq.Expressions;

namespace Ciizo.CleanPattern.Domain.Core.Repository
{
    public interface IReadRepository<TEntity> where TEntity : class
    {
        Task<TEntity?> GetByIdAsync(object id, CancellationToken cancellationToken);

        Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken, bool withTracking = false);

        IQueryable<TEntity> GetQueryable();
    }
}