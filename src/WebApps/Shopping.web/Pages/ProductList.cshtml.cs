namespace Shopping.web.Pages;

public class ProductListModel
    (ICatalogService catalogService,IBasketServices basketServices, ILogger<ProductListModel> logger)
    : PageModel
{
    public IEnumerable<string> CategoryList { get; set; } = [];

    public IEnumerable<ProductModel> ProductList { get; set; } = [];

    [BindProperty(SupportsGet = true)]
    public string SelectedCategory { get; set; } = default!;
    public async Task<IActionResult> OnGetAsync(string categoryName)
    {
        var response = await catalogService.GetProducts();

        CategoryList = response.Products.SelectMany(p => p.Category).Distinct();

        if (!string.IsNullOrWhiteSpace(categoryName))
        {
            ProductList = response.Products.Where(p=>p.Category.Contains(categoryName)).ToList();
            SelectedCategory = categoryName; 
        }
        else
        {
            ProductList = response.Products;
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAddToCartASync(Guid productId)
    {
        logger.LogInformation("Add to cart button clicked");
        var productResponse = await catalogService.GetProduct(productId);

        var basket = await basketServices.LoadUserBasket();

        basket.Items.Add(new ShoppingCartItemModel
        {
            ProductId = productId,
            ProductName = productResponse.Product.Name,
            Price = productResponse.Product.Price,
            Quantity = 1,
            Color = "Black"
        });

        await basketServices.StoreBasket(new StoreBasketRequest(basket));

        return RedirectToPage("Cart");
    }
}
