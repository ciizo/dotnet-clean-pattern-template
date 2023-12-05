using Ciizo.Restful.Onion.Domain.Core.Repository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Ciizo.Restful.Onion.Infrastructure.Persistence.Repository
{
    public class EfRepository<TEntity, TContext> : IRepository<TEntity, TContext>
        where TEntity : class
        where TContext : IApplicationDbContext
    {
        protected readonly TContext _dbContext;
        protected readonly DbSet<TEntity> _dbSet;

        public EfRepository(TContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<TEntity>();
        }

        public virtual void Insert(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public virtual void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        public virtual void Delete(TEntity entitiy)
        {
            _dbSet.Remove(entitiy);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAsync(
            Expression<Func<TEntity,
            bool>> predicate,
            CancellationToken cancellationToken,
            bool withTracking = false)
        {
            var query = _dbSet.Where(predicate);

            if (!withTracking)
            {
                query = query.AsNoTracking();
            }

            return await query.ToListAsync(cancellationToken: cancellationToken);
        }

        public virtual IQueryable<TEntity> GetQueryable()
        {
            return _dbSet.AsQueryable().AsNoTracking();
        }

        public virtual async Task<TEntity?> GetByIdAsync(object id, CancellationToken cancellationToken)
        {
            return await _dbSet.FindAsync(new object[] { id }, cancellationToken: cancellationToken);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _dbContext.SaveChangesAsync(cancellationToken: cancellationToken);
        }
    }
}