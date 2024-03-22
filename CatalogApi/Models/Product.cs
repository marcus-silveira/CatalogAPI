using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatalogApi.Models;

public class Product
{
    [Key]
    public int Id { get; private set; }
    
    [Required]
    [StringLength(80)]
    public string? Name { get; private set; }
    
    [Required]
    [StringLength(300)]
    public string? Description { get; private set; }
    
    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal Price { get; private set; }
    
    [Required]
    [StringLength(300)]
    public string? UrlImage { get; private set; }
    public float Inventory { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public int CategoryId { get; private set; }
    public Category? Category { get; set; }
}