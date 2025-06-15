
namespace Ordering.Application.Order.Queries.GetOrderByCustomer;

public class GetOrderByCustomerHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetOrderByCustomerQuery, GetOrderByCustomerResult>
{
    public async Task<GetOrderByCustomerResult> Handle(GetOrderByCustomerQuery query, CancellationToken cancellationToken)
    {
        // get orders by customer using dbContext
        // return result

        var orders = await dbContext.Orders
            .Include(x => x.OrderItems)
            .AsNoTracking()
            .Where(o => o.CustomerId == CustomerId.of(query.CustomerId))
            .OrderBy(o => o.OrderName.Value)
            .ToListAsync();

        return new GetOrderByCustomerResult(orders.ToOrderDtoList());
        
    }
}
