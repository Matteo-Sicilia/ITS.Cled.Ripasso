using System.ComponentModel.DataAnnotations;

namespace ITS.Cled.Ripasso.Model;

using Dapper.Contrib.Extensions;

[Table("Products")]
public class Product
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string Name { get; set; } = default!;

    [Required]
    [StringLength(10)]
    public string Code { get; set; } = default!;

    [Required]
    [Range(0, double.MaxValue)]
    public decimal Price { get; set; }
}