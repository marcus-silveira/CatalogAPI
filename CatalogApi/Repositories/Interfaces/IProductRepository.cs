using CatalogApi.Models;

namespace CatalogApi.Repositories.Interfaces;

public interface IProductRepository
{
    public Task<IQueryable<Product>> GetProductsAsync();
    public Task<Product?> GetProductAsync(int id);
    public Task<Product> CreateAsync(Product product);
    public Task<bool> UpdateAsync(Product product);
    public Task<bool> DeleteAsync(int id);
}