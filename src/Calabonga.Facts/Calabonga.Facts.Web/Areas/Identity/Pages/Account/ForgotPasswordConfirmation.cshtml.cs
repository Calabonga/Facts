using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Calabonga.Facts.Web.Areas.Identity.Pages.Account
{
    // Calabonga: WHAT I MADE 4
    [AllowAnonymous]
    public class ForgotPasswordConfirmation : PageModel
    {
        public void OnGet()
        {
        }
    }
}
