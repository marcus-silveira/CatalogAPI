using CatalogApi.Context;
using CatalogApi.Models;
using CatalogApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CatalogApi.Repositories.implementations;

public class CategoryRepository : ICategoryRepository
{
    private readonly CatalogApiDbContext _dbContext;


    public CategoryRepository(CatalogApiDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Category>> GetCategories()
    {
        return await _dbContext.Categories.ToListAsync();
    }

    public async Task<Category?> GetCategory(int id)
    {
        return await _dbContext.Categories.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Category> Create(Category category)
    {
         await _dbContext.Categories.AddAsync(category);
         await _dbContext.SaveChangesAsync();
         return category;
    }

    public Task<Category> Update(Category category)
    {
        throw new NotImplementedException();
    }

    public Task<Category> Delete(int id)
    {
        throw new NotImplementedException();
    }
}