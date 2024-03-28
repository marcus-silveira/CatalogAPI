using CatalogApi.Models;

namespace CatalogApi.Repositories.Interfaces;

public interface ICategoryRepository
{
    public Task<IEnumerable<Category>> GetCategories();
    public Task<Category?> GetCategory(int id);
    public Task<IEnumerable<Category>> GetCategoryAndProduct(int id);

    public Task<Category> Create(Category category);
    public Task<Category> Update(Category category);
    public Task<bool> Delete(int id);
}