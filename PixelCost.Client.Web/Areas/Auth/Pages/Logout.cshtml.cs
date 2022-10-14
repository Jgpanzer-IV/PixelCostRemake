using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PixelCost.Client.Web.Areas.Pages
{
    public class LogoutModel : PageModel
    {
        public SignOutResult OnGet()
        {
            return SignOut("cookies","oidc");
        }
    }
}
