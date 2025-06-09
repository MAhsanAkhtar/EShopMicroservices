namespace Ordering.Domain.ValueObjects;

public record CustomerId
{
    public Guid Value { get; set; }

    private CustomerId(Guid value) => Value = value;

    public static CustomerId of(Guid value)
    {
        ArgumentNullException.ThrowIfNull(value);

        if(value == Guid.Empty)
        {
            throw new DomainEventException("CustomerId cannot be empty.");
        }
        return new CustomerId(value);
    }
}
