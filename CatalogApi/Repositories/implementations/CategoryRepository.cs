using CatalogApi.Context;
using CatalogApi.Models;
using CatalogApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CatalogApi.Repositories.implementations;

public class CategoryRepository : Repository<Category>,ICategoryRepository
{
    private readonly CatalogApiDbContext _dbContext;


    public CategoryRepository(CatalogApiDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}