using CatalogApi.Context;
using CatalogApi.Repositories.Interfaces;

namespace CatalogApi.Repositories.implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CatalogApiDbContext _dbContext;
        private IProductRepository? _productRepository;
        private ICategoryRepository? _categoryRepository;
        
        public UnitOfWork(CatalogApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IProductRepository ProductRepository => _productRepository ??= new ProductRepository(_dbContext, null);
        public ICategoryRepository CategoryRepository => _categoryRepository ??= new CategoryRepository(_dbContext);
        
        public async Task Commit()
        {
            await _dbContext.SaveChangesAsync();
        }
        
        public async Task Dispose()
        {
            await _dbContext.DisposeAsync();
        }
    }
}