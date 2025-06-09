namespace Ordering.Domain.ValueObjects;

public record OrderItemId
{
    public Guid Value { get; }

    private OrderItemId(Guid value)=>Value = value;

    public static OrderItemId Of(Guid value)
    {
        // Domain Validaiton 
        ArgumentNullException.ThrowIfNull(value);
        if(value == Guid.Empty)
        {
            throw new DomainEventException("OrderItemId cannot be empty.");
        }

        // Returning object
        return new OrderItemId(value);
    }
}
