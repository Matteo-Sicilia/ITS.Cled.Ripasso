using ITS.Cled.Ripasso.Model;
using ITS.Cled.Ripasso.Services;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ITS.Cled.Ripasso.Endpoints;

public static class ProductsEndpoints
{
    public static IEndpointRouteBuilder MapProductsEndpoint(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/products").WithOpenApi().WithTags("Products");

        group.MapGet("/", GetProductsAsync);
        group.MapGet("/{id}", GetProductAsync);
        group.MapPost("/", InsertProductAsync);
        group.MapPut("/{id}", UpdateProductAsync);
        group.MapDelete("/{id}", DeleteProductAsync);

        return app;
    }

    public static async Task<Ok<IEnumerable<Product>>> GetProductsAsync(IProductDataService data)
    {
        var list = await data.GetProductsAsync();

        return TypedResults.Ok(list);
    }

    private static async Task<Results<Ok<Product>, NotFound>> GetProductAsync(
        int id,
        IProductDataService data
    )
    {
        var product = await data.GetProductById(id);

        if (product == null)
        {
            return TypedResults.NotFound();
        }

        return TypedResults.Ok(product);
    }

    public static async Task<Created<Product>> InsertProductAsync(
        Product product,
        IProductDataService data
    )
    {
        var newProduct = await data.CreateProduct(product);

        return TypedResults.Created($"/api/products/{newProduct.Id}", newProduct);
    }

    public static async Task<NoContent> UpdateProductAsync(
        int id,
        Product product,
        IProductDataService data
    )
    {
        product.Id = id;
        await data.UpdateProduct(product);

        return TypedResults.NoContent();
    }

    public static async Task<NoContent> DeleteProductAsync(int id, IProductDataService data)
    {
        await data.DeleteProduct(id);

        return TypedResults.NoContent();
    }
}