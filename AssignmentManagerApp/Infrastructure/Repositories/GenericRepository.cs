using System.Linq.Expressions;
using Entities.Abstraction;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class {
  protected readonly AppDbContext Context;
  protected readonly DbSet<TEntity> DbSet;

  protected GenericRepository(AppDbContext context) {
    Context = context;
    DbSet = context.Set<TEntity>();
  }
  
  public virtual async Task<IEnumerable<TEntity>> GetAllAsync(
    Expression<Func<TEntity, bool>>? filter = null,
    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
    string includeProperties = ""
  ) {
    IQueryable<TEntity> query = DbSet;

    if (filter is not null) {
      query = query.Where(filter);
    }

    var splitProperties = includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
    foreach (var includeProperty in splitProperties) {
      query = query.Include(includeProperty);
    }

    return orderBy is not null ? await orderBy(query).ToListAsync() : await query.ToListAsync();
  }

  public virtual async Task<TEntity?> GetByIdAsync(object id) {
    return await DbSet.FindAsync(id) ?? null;
  }

  public virtual async Task InsertAsync(TEntity entity) {
    await DbSet.AddAsync(entity);
  }

  public virtual Task UpdateAsync(TEntity entityToUpdate) {
    DbSet.Update(entityToUpdate);
    return Task.CompletedTask;
  }
  
  public virtual async Task DeleteAsync(object id) {
    var entityToDelete = await DbSet.FindAsync(id);
    if (entityToDelete is null) return;
    DbSet.Remove(entityToDelete);
  }

  public async Task SaveChangesAsync() {
    await Context.SaveChangesAsync();
  }
}