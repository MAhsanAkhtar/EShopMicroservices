namespace Ordering.Application.Order.Command.DeleteOrder;

public record DeleteOrderCommand(Guid orderId)
    :ICommand<DeleteOrderResult>
{
}

public record DeleteOrderResult(bool isSuccess);

public class DeleteOrderCommandValidator: AbstractValidator<DeleteOrderCommand>
{
    public DeleteOrderCommandValidator()
    {
        RuleFor(x => x.orderId).NotEmpty().WithMessage("OrderId is required");
    }
}
