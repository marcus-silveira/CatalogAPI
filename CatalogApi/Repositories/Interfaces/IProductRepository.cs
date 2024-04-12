using CatalogApi.Models;

namespace CatalogApi.Repositories.Interfaces;

public interface IProductRepository : IRepository<Product>
{
    public Task<IEnumerable<Product>> GetProductsByCategoryAsync(int id);
}