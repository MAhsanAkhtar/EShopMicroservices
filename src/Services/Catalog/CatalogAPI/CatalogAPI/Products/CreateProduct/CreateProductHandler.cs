namespace CatalogAPI.Products.CreateProduct;


using CatalogAPI.Models;

public record CreateProductCommand(string Name,List<string> Category,string Description, string ImageFile
    , decimal Price)
    :ICommand<CreateProductResult>;
public record CreateProductResult(Guid id);

internal class CreateProductCommandHandler(IDocumentSession session): 
    ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken tokken)
    {
        // Business logic to create a product
        // 1. Create Product Entity from command object
        // 2. Save to database.
        // 3. Return the result CreateProductResult

        var product = new Product
        {
            Name = command.Name,
            Category = command.Category,
            Description = command.Description,
            ImageFile = command.ImageFile,
            Price = command.Price
        };

        // TODO
        session.Store(product);
        await session.SaveChangesAsync();
        // Save to DB

        return new CreateProductResult(product.Id);
        
    }
}
