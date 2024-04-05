using System.Linq.Expressions;

namespace CatalogApi.Repositories.Interfaces;

public interface IRepository<T>
{
    public Task<IEnumerable<T>> GetAll();
    public Task<T?> Get(Expression<Func<T, bool>> predicate);
    public Task<T> Add(T entity);
    public Task<T> Update(T entity);
    public Task<T> Delete(T entity);
}