namespace Ordering.Application.Order.Queries.GetOrderByName;

public record GetOrderByNameQuery(string Name)
    :IQuery<GetOrderByNameResult>;

public record GetOrderByNameResult(IEnumerable<OrderDto> Orders);
