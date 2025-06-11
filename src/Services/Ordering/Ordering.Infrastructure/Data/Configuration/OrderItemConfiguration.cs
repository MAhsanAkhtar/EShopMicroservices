using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Infrastructure.Data.Configuration;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        // Primary Key Configuration
        builder.HasKey(oi => oi.Id);

        //Conversion method
        builder.Property(oi => oi.Id).HasConversion(
                                    orderItemId => orderItemId.Value,
                                    dbId => OrderItemId.Of(dbId));
        //Defining relation 
        builder.HasOne<Product>()
            .WithMany()
            .HasForeignKey(oi => oi.ProductId);

        builder.Property(oi => oi.Quantity).IsRequired();
        builder.Property(oi => oi.Price).IsRequired();


    }
}
