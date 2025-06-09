namespace Ordering.Domain.ValueObjects;

public record ProductId
{
    public Guid Value { get; }

    private ProductId(Guid value)=> Value = value;

    public static ProductId Of(Guid value)
    {
        // Domain Validation 
        ArgumentNullException.ThrowIfNull(value);
        if(value == Guid.Empty)
        {
            throw new DomainEventException("ProductId cannot be empty.");
        }

        // Returning object
        return new ProductId(value);
    }
}
