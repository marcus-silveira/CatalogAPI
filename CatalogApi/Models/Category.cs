using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatalogApi.Models;

[Table("Categories")]
public class Category
{
    [Key]
    public int Id { get; private set; }
    
    [Required]
    [StringLength(80)]
    public string? Name { get; private set; }
    
    [Required]
    [StringLength(300)]
    public string? UrlImage { get; private set; }

    public ICollection<Product>? Products { get; private set; } = new Collection<Product>();
}