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

    public async Task<IEnumerable<T>> GetAll()
    {
        return await DbContext.Set<T>().Take(10).ToListAsync();
    }

    public Task<T?> Get(Expression<Func<T, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public Task<T> Add(T entity)
    {
        throw new NotImplementedException();
    }

    public Task<T> Update(T entity)
    {
        throw new NotImplementedException();
    }

    public Task<T> Delete(T entity)
    {
        throw new NotImplementedException();
    }
}