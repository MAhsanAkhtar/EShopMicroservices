namespace Ordering.Application.Order.Queries.GetOrderByName;

public class GetOrderByNameHandler(IApplicationDbContext dbContext) 
    : IQueryHandler<GetOrderByNameQuery, GetOrderByNameResult>
{
    public async Task<GetOrderByNameResult> Handle(GetOrderByNameQuery query, CancellationToken cancellationToken)
    {
        // get orders by name using dbContext
        // return result
        var orders = await dbContext.Orders
            .Include(o=>o.OrderItems)
            .AsNoTracking()
            .Where(o=>o.OrderName.Value.Contains(query.Name))
            .OrderBy(o=>o.OrderName.Value)
            .ToListAsync(cancellationToken);
        
        return new GetOrderByNameResult(orders.ToOrderDtoList());
    }
}
