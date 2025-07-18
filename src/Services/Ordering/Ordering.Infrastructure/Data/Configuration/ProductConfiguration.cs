﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Infrastructure.Data.Configuration;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
   public void Configure(EntityTypeBuilder<Product> builder)
   {
        builder.HasKey(x => x.Id);

        builder.Property(p => p.Id).HasConversion(
            productid => productid.Value,
            dbId => ProductId.Of(dbId)
            );

        builder.Property(p => p.Name).HasMaxLength(100).IsRequired();
        
   }
}
