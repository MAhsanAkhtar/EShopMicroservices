
using System.Net;

namespace Shopping.web.Pages
{
    public class IndexModel(ICatalogService catalogService, IBasketServices basketServices, ILogger<IndexModel> logger)
        : PageModel
    {
        public IEnumerable<ProductModel> ProductList { get; set; }= new List<ProductModel>();

   
        public async Task<IActionResult> OnGetAsync()
        {
            logger.LogInformation("Index page visited");
            var result = await catalogService.GetProducts();
            //var result = await catalogService.GetProducts(2,3);
            ProductList = result.Products;
            return Page();
        }

        public async Task<IActionResult> OnPostAddToCartAsync(Guid productId)
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
}
