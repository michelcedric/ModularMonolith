using System.Linq.Expressions;
using My.ModularMonolith.Common.Domain.Entities;

namespace My.ModularMonolith.Common.Domain.Interfaces.Repositories;

public interface IAsyncRepository<TEntity, in TKey>
    where TEntity : BaseEntity<TKey>
    where TKey : notnull
{
    Task<TEntity?> GetByIdAsync(TKey id, CancellationToken cancellationToken);
    Task<IReadOnlyList<TEntity>> ListAllAsync(CancellationToken cancellationToken);
    Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken);
    Task<TEntity> AddWithoutSaveAsync(TEntity entity, CancellationToken cancellationToken);
    Task<IEnumerable<TEntity>> AddAsync(IList<TEntity> entities, CancellationToken cancellationToken);
    Task UpdateAsync(TEntity entity, CancellationToken cancellationTok);
    Task UpdateAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToke);
    Task<bool> ExecuteDeleteAsync(TKey id, CancellationToken cancellationToken);
    Task<bool> ExecuteDeleteAsync(TKey[] ids, CancellationToken cancellationToken);
    Task DeleteAsync(TEntity entity, CancellationToken cancellationToken);
    Task DeleteAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken);
    Task<IEnumerable<TEntity>> FindByAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);
    Task<int> CountByAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);
    Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);
    IQueryable<TEntity> GetAllAsQueryable();
    IQueryable<TEntity> GetAsQueryable(Expression<Func<TEntity, bool>> predicate);
}