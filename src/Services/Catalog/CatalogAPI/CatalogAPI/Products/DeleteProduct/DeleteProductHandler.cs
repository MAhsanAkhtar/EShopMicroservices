

using System.Data;

namespace CatalogAPI.Products.DeleteProduct;

public record DeleteProductCommand(Guid Id): ICommand<DeleteProductResult>;
public record DeleteProductResult(bool success);

public class DeleteProductCommandValidator: AbstractValidator<DeleteProductCommand> 
{
    public DeleteProductCommandValidator()
    {
        RuleFor(item => item.Id).NotEmpty().WithMessage("Product ID is reuqired");
    }
}
internal class DeleteProductCommandHandler
    (IDocumentSession session)
    : ICommandHandler<DeleteProductCommand, DeleteProductResult>
{
    public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {

        session.Delete<Product>(command.Id);
        await session.SaveChangesAsync(cancellationToken); ;

        return new DeleteProductResult(true);

    }
}
