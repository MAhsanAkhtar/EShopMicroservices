



using Discount.Grpc;
using Microsoft.VisualBasic;

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
public class StoreBasketCommandHandler
    (IBasketRepository repository, DiscountProtoService.DiscountProtoServiceClient discountProto) :
    ICommandHandler<StoreBasketComand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketComand command, CancellationToken cancellationToken)
    {
        ShoppingCart cart = command.Cart;

        //TODO: Communicate with Discount.Grpc and Caculate the latest prices of product with their discount
        await DeductDiscount(discountProto, command, cancellationToken);
        // Store basket in the database (use Marten upsert - if exist = update, if not exist insert)
        await repository.StoreBasket(command.Cart, cancellationToken);
        return new StoreBasketResult(command.Cart.UserName);
    }

    private async Task DeductDiscount(DiscountProtoService.DiscountProtoServiceClient discountProto, StoreBasketComand command, CancellationToken cancellationToken)
    {
        foreach (var item in command.Cart.Items)
        {
            var coupon = await discountProto.GetDiscountAsync(new GetDiscountRequest { ProductName = item.ProductName }, cancellationToken: cancellationToken);
            item.Price -= coupon.Amount;
        }
    }
}
