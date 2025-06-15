namespace Ordering.Application.Dtos;

public record OrderDto(
    Guid id,
    Guid CustomerId,
    string OrderName,
    AddressDto ShippingAddress,
    AddressDto BillingAddress,
    PaymentDto Payment, 
    OrderStatus Status,
    List<OrderItemDto> OrderItems);