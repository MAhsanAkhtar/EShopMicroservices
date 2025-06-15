using BuildingBlocks.Pagination;

namespace Ordering.Application.Order.Queries.GetOrders;

public record GetOrdersQuery(PaginationRequest PaginatedResult)
    :IQuery<GetOrderResult>;

public record GetOrderResult(PaginatedResult<OrderDto> Orders);
