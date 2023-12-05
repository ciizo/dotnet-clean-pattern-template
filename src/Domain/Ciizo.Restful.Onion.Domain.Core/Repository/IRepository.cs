namespace Ciizo.Restful.Onion.Domain.Core.Repository
{
    public interface IRepository<TEntity, TContext> : IWriteRepository<TEntity>, IReadRepository<TEntity>, IDisposable
        where TEntity : class
    {
    }
}