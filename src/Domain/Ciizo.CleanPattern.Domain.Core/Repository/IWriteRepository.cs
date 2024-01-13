namespace Ciizo.CleanPattern.Domain.Core.Repository
{
    public interface IWriteRepository<TEntity> where TEntity : class
    {
        Task SaveChangesAsync(CancellationToken cancellationToken);

        void Insert(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);

        void DeleteById(object id);
    }
}