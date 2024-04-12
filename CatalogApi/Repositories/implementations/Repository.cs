using System.Linq.Expressions;
using CatalogApi.Context;
using CatalogApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CatalogApi.Repositories.implementations;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly CatalogApiDbContext DbContext;
    public Repository(CatalogApiDbContext dbContext)
    {
        DbContext = dbContext;
    }

    public Task<IQueryable<T>> GetAll()
    {
        return Task.FromResult(DbContext.Set<T>().AsNoTracking());
    }

    public async Task<T?> Get(Expression<Func<T, bool>> predicate)
    {
        return await DbContext.Set<T>().FirstOrDefaultAsync(predicate);
    }

    public async Task<T> Create(T entity)
    {
        await DbContext.Set<T>().AddAsync(entity);
        // await DbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<T> Update(T entity)
    {
        DbContext.Set<T>().Update(entity);
        // await DbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<T> Delete(T entity)
    {
        DbContext.Set<T>().Remove(entity);
        // await DbContext.SaveChangesAsync();
        return entity;
    }
}