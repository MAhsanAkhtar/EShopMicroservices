
using BuildingBlocks.Pagination;
using System.Runtime.InteropServices;

namespace Ordering.Application.Order.Queries.GetOrders;

public class GetOrdersHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetOrdersQuery, GetOrderResult>
{
    public async Task<GetOrderResult> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
    {
        // get order with pagination 
        // return result
        
        var pageIndex = request.PaginatedResult.PageIndex;
        var pageSize = request.PaginatedResult.PageSize;

        var totalCount = await dbContext.Orders.LongCountAsync(cancellationToken);

        var orders = await dbContext.Orders
            .Include(o => o.OrderItems)
            .OrderBy(o => o.OrderName.Value)
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .ToListAsync(cancellationToken);


        return new GetOrderResult(
            new PaginatedResult<OrderDto>(
                pageIndex,
                pageSize,
                totalCount,
                orders.ToOrderDtoList()));
            
    }
}
