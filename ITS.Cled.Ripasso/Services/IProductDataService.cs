using ITS.Cled.Ripasso.Model;

namespace ITS.Cled.Ripasso.Services;

public interface IProductDataService
{
    Task<IEnumerable<Product>> GetProductsAsync();
    Task<Product?> GetProductById(int id);
    Task<Product> CreateProduct(Product product);
    Task UpdateProduct(Product product);
    Task DeleteProduct(int id);
}