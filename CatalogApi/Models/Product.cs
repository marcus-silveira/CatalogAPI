using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using CatalogApi.Validations;

namespace CatalogApi.Models;

public class Product : IValidatableObject
{
    // MIGRAR PARA FLUENT VALIDATIONS
    [Key] public int Id { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório")]
    [StringLength(80)] [FirstLetterCapitalized]
    public string? Name { get; set; }

    [Required] [StringLength(300)] public string? Description { get; set; }

    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal Price { get; set; }

    [Required] [StringLength(300)] public string? UrlImage { get; set; }

    public float Inventory { get; set; }
    public DateTime CreatedAt { get; private set; } = DateTime.Now;

    public int CategoryId { get; set; }

    [JsonIgnore] public Category? Category { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        // Usando no validations
        // if (!string.IsNullOrEmpty(Name))
        // {
        //     var firstLetter = Name[0].ToString();
        //     if (firstLetter != firstLetter.ToUpper())
        //     {
        //         yield return new
        //             ValidationResult("A primeira letra do produto deve ser maiúscula",
        //                 new[] { nameof(Name) });
        //     }
        // }
        
        if (Inventory <= 0)
        {
            yield return new
                ValidationResult("O estoque deve ser maior que zero",
                    new[]
                        { nameof(Inventory) }
                );
        }
    }
}