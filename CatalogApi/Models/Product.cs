namespace CatalogApi.Models;

public class Product
{
    public int Id { get; private set; }
    public string? Name { get; private set; }
    public string? Description { get; private set; }
    public decimal Price { get; private set; }
    public string? UrlImage { get; private set; }
    public float Inventory { get; private set; }
    public DateTime CreatedAt { get; private set; }
}