using CatalogApi.Context;
using CatalogApi.Models;
using CatalogApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CatalogApi.Repositories.implementations;

public class ProductRepository : IProductRepository
{
    private readonly CatalogApiDbContext _dbContext;
    private readonly ILogger<ProductRepository> _logger;

    public ProductRepository(CatalogApiDbContext catalogApiDbContext, ILogger<ProductRepository> logger)
    {
        _dbContext = catalogApiDbContext;
        _logger = logger;
    }

    public Task<IQueryable<Product>> GetProductsAsync()
    {
        return Task.FromResult<IQueryable<Product>>(_dbContext.Products);
    }

    public async Task<Product?> GetProductAsync(int id)
    {
        return await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Product> CreateAsync(Product product)
    {
        try
        {
            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            _logger.LogError("Ocorreu um erro ao atualizar criar o produto");
            throw;
        }

        return product;
    }

    public async Task<bool> UpdateAsync(Product updatedProduct)
    {
        try
        {
            var existingProduct = await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == updatedProduct.Id);
            if (existingProduct == null)
                return false;

            _dbContext.Entry(existingProduct).CurrentValues.SetValues(updatedProduct);
            // _dbContext.Update(updatedProduct);

            await _dbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Ocorreu um erro ao atualizar o produto: {ex.Message}");
            throw;
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        try
        {
            var productExists = await _dbContext.Products.AnyAsync(x => x.Id == id);
            if (!productExists) return false;

            var product = new Product { Id = id };
            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError($"Erro ao tentar exluir o Produto de ID: {id}");
            throw;
        }
    }
}