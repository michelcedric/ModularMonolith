using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using My.ModularMonolith.Common.Domain.Entities;
using My.ModularMonolith.Common.Domain.Interfaces.Repositories;

namespace My.ModularMonolith.Common.Infrastructure.Data.Repositories;

[ExcludeFromCodeCoverage]
public abstract class EfRepository<TEntity, TKey, TDbContext> :
    IAsyncRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    where TDbContext : DbContext
    where TKey : notnull
{
    private readonly DbSet<TEntity> _dbSet;
    private readonly TDbContext _dbContext;

    protected EfRepository(TDbContext dbContext)
    {
        _dbSet = dbContext.Set<TEntity>();
        _dbContext = dbContext;
    }

    public async Task<TEntity?> GetByIdAsync(TKey id, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(id);
        var values = new object[]
        {
            id
        };
        return await _dbSet.FindAsync(values, cancellationToken: cancellationToken);
    }

    public async Task<IEnumerable<TEntity>> FindByAsync(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken)
    {
        return await _dbSet.Where(predicate).ToListAsync(cancellationToken);
    }

    public async Task<int> CountByAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
    {
        return await _dbSet.CountAsync(predicate, cancellationToken);
    }

    public async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken)
    {
        return await _dbSet.FirstOrDefaultAsync(predicate, cancellationToken);
    }

    public virtual async Task<IReadOnlyList<TEntity>> ListAllAsync(CancellationToken cancellationToken)
    {
        return await _dbSet.ToListAsync(cancellationToken);
    }

    public virtual async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await _dbSet.AddAsync(entity, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<TEntity> AddWithoutSaveAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await _dbSet.AddAsync(entity, cancellationToken);
        return entity;
    }

    public async Task<IEnumerable<TEntity>> AddAsync(IList<TEntity> entities, CancellationToken cancellationToken)
    {
        await _dbSet.AddRangeAsync(entities, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return entities.ToList();
    }

    public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        _dbContext.Set<TEntity>().Update(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken)
    {
        _dbContext.Set<TEntity>().UpdateRange(entities);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> ExecuteDeleteAsync(TKey id, CancellationToken cancellationToken)
    {
        if (!_dbContext.Database.IsRelational())
        {
            var entity = await GetByIdAsync(id, cancellationToken);
            if (entity != null)
            {
                await DeleteAsync(entity, cancellationToken);
                return true;
            }

            return false;
        }

        var number = await _dbSet.Where(e => e.Id.Equals(id)).ExecuteDeleteAsync(cancellationToken);
        return number == 1;
    }

    public async Task<bool> ExecuteDeleteAsync(TKey[] ids, CancellationToken cancellationToken)
    {
        if (!_dbContext.Database.IsRelational())
        {
            var entities = (await FindByAsync((e) => ids.Contains(e.Id), cancellationToken)).ToArray();
            if (entities.Length != ids.Length)
            {
                return false;
            }

            await DeleteAsync(entities, cancellationToken);
            return true;
        }

        var number = await _dbSet.Where(e => ids.Contains(e.Id)).ExecuteDeleteAsync(cancellationToken);
        return number == ids.Length;
    }

    public virtual async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken)
    {
        _dbSet.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken)
    {
        _dbSet.RemoveRange(entities);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public IQueryable<TEntity> GetAllAsQueryable()
    {
        return _dbSet.AsQueryable().AsNoTracking();
    }

    public IQueryable<TEntity> GetAsQueryable(Expression<Func<TEntity, bool>> predicate)
    {
        return _dbSet.AsQueryable().Where(predicate);
    }
}