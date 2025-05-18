



namespace Basket.API.Basket.StoreBasket;

public record StoreBasketComand(ShoppingCart Cart): ICommand<StoreBasketResult>;

public record StoreBasketResult(string UserName);

public class StoreBasketCommandValidator: AbstractValidator<StoreBasketComand>
{
    public StoreBasketCommandValidator()
    {
        RuleFor(x => x.Cart).NotNull().WithMessage("Cart can not be null");
        RuleFor(x => x.Cart.UserName).NotEmpty().WithMessage("UserName is required");
    }
}
public class StoreBasketCommandHandler(IBasketRepository repository) :
    ICommandHandler<StoreBasketComand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketComand command, CancellationToken cancellationToken)
    {
        ShoppingCart cart = command.Cart;

        //TODO: store basket in db
        //TODO:  update cache

        await repository.StoreBasket(command.Cart,cancellationToken);
        return new StoreBasketResult(command.Cart.UserName);
    }
}
