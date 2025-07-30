namespace Shopping.web.Pages;

public class OrderListModel
    (IOrderingService orderingService, ILogger<OrderListModel> logger)
    : PageModel
{
    public IEnumerable<OrderModel> Orders { get; set; } = default!;
    public async Task<IActionResult> OnGet()
    {
        // assumption custoemrId is passed from the UI authenticated user swn
        var customerId = new Guid("58c49479-ec65-4de2-86e7-033c546291aa");

        var response = await orderingService.GetOrdersByCustomer(customerId);
        Orders = response.orders;
        return Page();
    }
}
