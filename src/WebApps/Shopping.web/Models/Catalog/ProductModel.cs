﻿using Microsoft.AspNetCore.Authentication;

namespace Shopping.web.Models.Catalog;

public class ProductModel
{
    public Guid Id { get; set; }

    public string Name { get; set; } = default!;

    public List<string> Category { get; set; } = new();

    public string Description { get; set; } = default!;

    public string ImageFile { get; set; } = default!;

    public decimal Price { get; set; }
}

//Wrapper Classes

public record GetProductsResponse(IEnumerable<ProductModel> Products);

public record GetProductsByCategoryResponse(IEnumerable<ProductModel> Products);    

public record GetProductByIdResponse(ProductModel Product);