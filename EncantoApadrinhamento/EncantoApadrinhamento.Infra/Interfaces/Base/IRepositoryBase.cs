using System.Linq.Expressions;

namespace EncantoApadrinhamento.Infra.Interfaces.Base
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        // Query Methods
        IQueryable<TEntity> FindAll(bool asNoTracking = false);
        IQueryable<TEntity> FindAll(Expression<Func<TEntity, bool>>? expression = null, bool asNoTracking = false);
        Task<IEnumerable<TEntity>> FindAllAsync(bool asNoTracking = false, CancellationToken cancellationToken = default);
        Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>>? expression = null, bool asNoTracking = false, CancellationToken cancellationToken = default);

        // Grouping Methods
        Task<List<TResult>> GroupByAsync<TKey, TResult>(
            Expression<Func<TEntity, bool>> expression,
            Expression<Func<TEntity?, TKey>> keySelector,
            Expression<Func<TKey, IEnumerable<TEntity?>, TResult>> resultSelector,
            CancellationToken cancellationToken);

        Task<List<TResult>> GroupByAsync<TKey, TResult>(
            IQueryable<TEntity> query,
            Expression<Func<TEntity, TKey>> keySelector,
            Expression<Func<TKey, IEnumerable<TEntity>, TResult>> resultSelector,
            CancellationToken cancellationToken);

        // Single Result Methods
        Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression, bool asNoTracking = false, bool ignoreFindAll = false, CancellationToken cancellationToken = default);
        Task<TResult?> FirstOrDefaultAsync<TResult>(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, TResult>> selector, bool asNoTracking = false, bool ignoreFindAll = false, CancellationToken cancellationToken = default);

        // Existence Check
        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken);

        // Create Methods
        void Create(TEntity entity);
        Task CreateAndSaveAsync(TEntity entity, CancellationToken cancellationToken);

        // Update Methods
        void Update(TEntity entity);
        Task UpdateAndSaveAsync(TEntity entity, CancellationToken cancellationToken);

        // Delete Methods
        void Delete(TEntity entity);
        Task DeleteAndSaveAsync(TEntity entity, CancellationToken cancellationToken);
        void DeleteRange(IEnumerable<TEntity> entities);
        Task DeleteRangeAndSaveAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken);

        // Save Changes
        Task SaveAsync(CancellationToken cancellationToken);
    }
}
