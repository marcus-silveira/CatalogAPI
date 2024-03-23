using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CatalogApi.Models;

public class Product
{
    [Key] public int Id { get; set; }

    [Required] [StringLength(80)] public string? Name { get; set; }

    [Required] [StringLength(300)] public string? Description { get; set; }

    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal Price { get; set; }

    [Required] [StringLength(300)] public string? UrlImage { get; set; }

    public float Inventory { get; set; }
    public DateTime CreatedAt { get; private set; } = DateTime.Now;

    public int CategoryId { get; set; }

    [JsonIgnore] public Category? Category { get; set; }
}