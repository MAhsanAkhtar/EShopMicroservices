﻿
namespace CatalogAPI.Products.CreateProduct;

public record CreateProductRequest(string Name, List<string> Category, string Description, string ImageFile
    , decimal Price);

public record CreateProductResponse(Guid id);

public class CreateProductEndpoint: ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/products",
            async (CreateProductRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateProductCommand>();
                var result = await sender.Send(command); // trigger handle class

                var response = result.Adapt<CreateProductResponse>();
                return Results.Created($"/products/{response.id}", response);
            })
            .WithName("CreateProduct")
            .Produces<CreateProductResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create Product")
            .WithDescription("Create Product");
        
        
    }
}
