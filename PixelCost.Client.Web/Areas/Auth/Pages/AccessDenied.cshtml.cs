using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PixelCost.Client.Web.Areas.Pages
{
    public class AccessDeniedModel : PageModel
    {
        public RedirectToPageResult OnGet()
        {
            return RedirectToPagePermanent("Index");
        }
    }
}
