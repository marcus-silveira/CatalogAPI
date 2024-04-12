using CatalogApi.Context;
using CatalogApi.Models;
using CatalogApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CatalogApi.Repositories.implementations;

public class ProductRepository : Repository<Product>, IProductRepository
{
    private readonly CatalogApiDbContext _dbContext;
    private readonly ILogger<ProductRepository> _logger;

    public ProductRepository(CatalogApiDbContext catalogApiDbContext, ILogger<ProductRepository> logger) : base(catalogApiDbContext)
    {
        _dbContext = catalogApiDbContext;
        _logger = logger;
    }

    public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(int id)
    {
        return (await GetAll()).Where(x => x.CategoryId == id).Take(10).ToList();
    }
}