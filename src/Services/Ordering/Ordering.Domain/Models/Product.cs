using Ordering.Domain.Abstractions;

namespace Ordering.Domain.Models;

public class Product: Entity<ProductId>
{
    public string Name { get; private set; } = default!;

    public decimal Price { get; private set; }= default!;

    // Create Method public static
    public static Product Create(ProductId id,string name, decimal price)
    {
        //Domain Validation
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);

        // Returning object
        var product = new Product
        {
            Id = id,
            Name = name,
            Price = price
        };
        return product;
    }
}
