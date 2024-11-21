namespace ITS.Cled.Ripasso.Services;

using Dapper;
using Dapper.Contrib.Extensions;
using ITS.Cled.Ripasso.Model;
using Microsoft.Data.SqlClient;

public class ProductsDataService : IProductDataService
{
    private readonly string _connectionString;

    public ProductsDataService(IConfiguration configuration)
    {
        _connectionString =
            configuration.GetConnectionString("db")
            ?? throw new Exception("Missing connectionString 'db'.");
    }

    public async Task<IEnumerable<Product>> GetProductsAsync()
    {
        using var connection = new SqlConnection(_connectionString);
        const string query = """
            SELECT
                id,
                name,
                code,
                price
            FROM products;
            """;
        return await connection.QueryAsync<Product>(query);
    }

    public async Task<Product?> GetProductById(int id)
    {
        using var connection = new SqlConnection(_connectionString);
        const string query = """
            SELECT
                id,
                name,
                code,
                price
            FROM products
            WHERE id = @id;
            """;
        return await connection.QueryFirstOrDefaultAsync<Product>(query, new { id });
    }

    public async Task<Product> CreateProduct(Product product)
    {
        using var connection = new SqlConnection(_connectionString);
        const string query = """
            INSERT INTO products (name, code, price)
            VALUES (@Name, @Code, @Price)  
            """;
        await connection.ExecuteAsync(query, product);

        return product;
    }

    public async Task UpdateProduct(Product product)
    {
        using var connection = new SqlConnection(_connectionString);
        const string query = """
            UPDATE products
            SET
                name = @Name,
                code = @Code,
                price = @Price
            WHERE id = @Id
            """;
        await connection.ExecuteAsync(query, product);
    }

    public async Task DeleteProduct(int id)
    {
        using var connection = new SqlConnection(_connectionString);

        const string query = """
            DELETE FROM products WHERE id = @id;
            """;
        await connection.ExecuteAsync(query, new { id });
    }
}