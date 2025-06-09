namespace Ordering.Domain.ValueObjects;

public record OrderId
{
    public Guid Value { get; }

    private OrderId(Guid value)=>Value = value;

    public static OrderId Of(Guid value)
    {
        //Domain Validation
        ArgumentNullException.ThrowIfNull(value);
        if(value == Guid.Empty)
        {
            throw new DomainEventException("OrderId cannot be empty.");
        }

        //Returning Object
        return new OrderId(value);

    }

}
