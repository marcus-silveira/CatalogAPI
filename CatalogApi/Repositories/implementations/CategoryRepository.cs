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

    public async Task<IEnumerable<Category>> GetCategoryAndProduct(int id)
    {
        return await _dbContext.Categories.Include(p => p.Products).Where(x => x.Id == id).ToListAsync();
    }

    public async Task<Category> Create(Category category)
    {
        await _dbContext.Categories.AddAsync(category);
        await _dbContext.SaveChangesAsync();
        return category;
    }

    public async Task<Category> Update(Category category)
    {
        _dbContext.Entry(category).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
        return category;
    }

    public async Task<bool> Delete(int id)
    {
        var category = await _dbContext.Categories.FindAsync(id);
        if (category is null)
        {
            return false;
        }
        _dbContext.Categories.Remove(category);
        await _dbContext.SaveChangesAsync();
        return true;
    }
}