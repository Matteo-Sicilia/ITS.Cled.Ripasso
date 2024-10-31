namespace ITS.Cled.Ripasso.Model;

using Dapper.Contrib.Extensions;

[Table("Products")]
public class Product
{
	public int Id { get; set; }
	public string Name { get; set; } = default!;
	public string Code { get; set; } = default!;
	public decimal Price { get; set; } = default!;
}