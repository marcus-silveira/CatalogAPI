namespace CatalogApi.Repositories.Interfaces;

public interface IUnitOfWork
{
    public IProductRepository ProductRepository { get; }
    public ICategoryRepository CategoryRepository { get; }

    Task Commit();
}