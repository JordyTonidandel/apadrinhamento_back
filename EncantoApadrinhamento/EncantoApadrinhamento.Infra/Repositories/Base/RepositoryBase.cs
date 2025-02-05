using EncantoApadrinhamento.Infra.Context;
using EncantoApadrinhamento.Infra.Extensions;
using EncantoApadrinhamento.Infra.Interfaces.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EncantoApadrinhamento.Infra.Repositories.Base
{
    public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        protected AppDBContext Context { get; }
        protected DbSet<TEntity> DbSet { get; }

        public RepositoryBase(AppDBContext context)
        {
            Context = context;
            DbSet = Context.Set<TEntity>();
        }

        public virtual IQueryable<TEntity> FindAll(bool asNoTracking = false)
        {
            var query = DbSet.ApplyTracking(asNoTracking);

            return query;
        }

        public virtual IQueryable<TEntity> FindAll(Expression<Func<TEntity, bool>>? expression = null, bool asNoTracking = false)
        {
            var query = DbSet.ApplyTracking(asNoTracking);

            if (expression != null)
                query = query.Where(expression);

            return query;
        }
        public async Task<IEnumerable<TEntity>> FindAllAsync(bool asNoTracking = false, CancellationToken cancellationToken = default)
        {
            return await FindAll(asNoTracking)
                .ToListAsync(cancellationToken: cancellationToken)
                .ConfigureAwait(false);
        }
        public async Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>>? expression = null, bool asNoTracking = false, CancellationToken cancellationToken = default)
        {
            return await FindAll(expression, asNoTracking)
                .ToListAsync(cancellationToken: cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<List<TResult>> GroupByAsync<TKey, TResult>(Expression<Func<TEntity, bool>> expression,
            Expression<Func<TEntity?, TKey>> keySelector, Expression<Func<TKey, IEnumerable<TEntity?>, TResult>> resultSelector, CancellationToken cancellationToken)
        {
            return await FindAll(expression, true)
                .GroupBy(keySelector, resultSelector)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<List<TResult>> GroupByAsync<TKey, TResult>(IQueryable<TEntity> query,
            Expression<Func<TEntity, TKey>> keySelector, Expression<Func<TKey, IEnumerable<TEntity>, TResult>> resultSelector, CancellationToken cancellationToken)
        {
            return await query
                .GroupBy(keySelector, resultSelector)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression, bool asNoTracking = false, bool ignoreFindAll = false, CancellationToken cancellationToken = default)
        {
            var query = ignoreFindAll
                ? DbSet.AsQueryable()
                : FindAll(asNoTracking);

            return await query.ApplyTracking(asNoTracking)
                .FirstOrDefaultAsync(expression, cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<TResult?> FirstOrDefaultAsync<TResult>(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, TResult>> selector,
            bool asNoTracking = false, bool ignoreFindAll = false, CancellationToken cancellationToken = default)
        {
            var query = ignoreFindAll
                ? DbSet.AsQueryable()
                : FindAll(asNoTracking);

            return await query.ApplyTracking(asNoTracking)
                .Where(expression)
                .Select(selector)
                .FirstOrDefaultAsync(cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken)
        {
            return await FindAll(expression).AnyAsync(cancellationToken).ConfigureAwait(false); ;
        }

        public async virtual void Create(TEntity entity)
        {
            await DbSet.AddAsync(entity);
        }

        public virtual async Task CreateAndSaveAsync(TEntity entity, CancellationToken cancellationToken)
        {
            Create(entity);

            await SaveAsync(cancellationToken).ConfigureAwait(false);
        }

        public virtual void Update(TEntity entity)
        {
            DbSet.Update(entity);
        }

        public virtual async Task UpdateAndSaveAsync(TEntity entity, CancellationToken cancellationToken)
        {
            Update(entity);

            await SaveAsync(cancellationToken).ConfigureAwait(false);
        }

        public virtual void Delete(TEntity entity)
        {
            DbSet.Remove(entity);
        }

        public virtual async Task DeleteAndSaveAsync(TEntity entity, CancellationToken cancellationToken)
        {
            DbSet.Remove(entity);

            await SaveAsync(cancellationToken).ConfigureAwait(false);
        }

        public virtual void DeleteRange(IEnumerable<TEntity> entities)
        {
            DbSet.RemoveRange(entities);
        }

        public virtual async Task DeleteRangeAndSaveAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken)
        {
            DbSet.RemoveRange(entities);

            await SaveAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task SaveAsync(CancellationToken cancellationToken)
        {
            await Context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}
