namespace Ordering.Application.Order.Command.UpdateOrder;

public record UpdateOrderResult(bool IsSuccess);
public record UpdateOrderCommand(OrderDto Order)
    :ICommand<UpdateOrderResult>;


public class UpdateOrderCommandValidator: AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidator()
    {
        RuleFor(x => x.Order.id).NotEmpty().WithMessage("Id is required");
        RuleFor(x => x.Order.OrderName).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.Order.CustomerId).NotNull().WithMessage("CustomerId is required");
    }
}