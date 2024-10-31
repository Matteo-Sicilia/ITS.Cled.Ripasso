using Dapper.Contrib.Extensions;
using ITS.Cled.Ripasso.Model;

namespace ITS.Cled.Ripasso.Services;

using Dapper;
using Dapper.Contrib;
using Npgsql;

public class ProductsDataService : IProductDataService
{
    private readonly string _connectionString;

    public ProductsDataService(IConfiguration configuration)
    {
        _connectionString =
            configuration.GetConnectionString("db")
            ?? throw new Exception("Missing connection string 'db'");
    }

    public async Task<IEnumerable<Product>> GetProductsAsync()
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        return await connection.GetAllAsync<Product>();
    }

    public async Task<Product?> GetProductById(int id)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        return await connection.GetAsync<Product>(id);
    }

    public async Task<Product> CreateProduct(Product product)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.InsertAsync(product);

        return product;
    }

    public async Task UpdateProduct(Product product)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.UpdateAsync(product);
    }

    public async Task DeleteProduct(int id)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        const string query = "DELETE FROM Products WHERE Id = @Id";

        await connection.ExecuteAsync(query, new { id });
    }
}