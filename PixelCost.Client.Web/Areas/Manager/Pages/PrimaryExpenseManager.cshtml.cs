using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace PixelCost.Client.Web.Areas.Pages
{
    public class PrimaryExpenseManager : PageModel
    {
    
        [BindProperty]
        public string SelectedTerm{get;set;} = "None";


        public PageResult OnGet()
        {
        

            return Page();
        }

        public IActionResult OnPost()
        {
            if(ModelState.IsValid){
                return RedirectToPage("MainMenu", new{area = "Menu"});
            }else
                return Page();
        }
    }
}
