namespace Shopping.web.Pages;

public class CheckoutModel
    (IBasketServices basketServices, ILogger<CheckoutModel> logger)
    : PageModel
{
    [BindProperty]
    public BasketCheckoutModel Order { get; set; } = default!;

    public ShoppingCartModel Cart { get; set; } = default!;
    public async Task<IActionResult> OnGetAsync()
    {
        Cart = await basketServices.LoadUserBasket();

        return Page();

    }

    public async Task<IActionResult> OnPostCheckOutAsync()
    {
        logger.LogInformation("Checkout button is clicked");

        Cart = await basketServices.LoadUserBasket();

        if (!ModelState.IsValid)
        {
            return Page();
        }

        // assumption customerId passewd in from the UI authenticate user swn
        Order.CustomerId = new Guid("58c49479-ec65-4de2-86e7-033c546291aa");
        Order.UserName = Cart.UserName;
        Order.TotalPrice = Cart.TotalPrice;

        await basketServices.CheckoutBasket(new CheckoutBasketRequest(Order));

        return RedirectToPage("Confirmation", "OrderSubmitted");
    }
}
