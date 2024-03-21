using CatalogApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CatalogApi.Context;

public class CatalogApiDbContext : DbContext
{
    public CatalogApiDbContext(DbContextOptions<CatalogApiDbContext> options) : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
}