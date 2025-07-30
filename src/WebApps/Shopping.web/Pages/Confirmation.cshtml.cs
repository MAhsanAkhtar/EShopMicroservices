using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Shopping.web.Pages
{
    public class ConfirmationModel : PageModel
    {
        public string Message { get; set; } = default!;
        public void OnGetContact()
        {
            Message = "Your email was sent.";
        }
        
        public void OnGetOrderSubmitted()
        {
            Message = "Your oder submitted successfully.";
        }
    }
}
