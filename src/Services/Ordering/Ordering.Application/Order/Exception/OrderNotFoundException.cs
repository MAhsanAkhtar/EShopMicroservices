
namespace Ordering.Application.Order.Exception;

public class OrderNotFoundException: NotFoundException
{
    public OrderNotFoundException(Guid id) : base("Order", id)
    {
    }
}
