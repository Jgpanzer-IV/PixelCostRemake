using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace PixelCost.Client.Web.Areas.Pages
{
    public class SubDurationManager : PageModel
    {
        
        [BindProperty]
        public SubDuration SubDuration {get;set;} = new();

        [BindProperty]
        [Display(Name = "Select DurationForm")]
        public string? DurationID {get;set;}

        

        public PageResult OnGet()
        {
            return Page();
        }

        public IActionResult OnPost(){
            if (this.ModelState.IsValid){
                return RedirectToPage("MainMenu",new {area = "Menu"});
            }
            return Page();
        }
    }


    public class SubDuration{
        
        [Required]
        public string? Name {get; set;}

        [DataType(DataType.Currency)]
        [Required]
        public decimal? Cost {get;set;}

        [DataType(DataType.Date)]
        [Required]
        public DateTime? StartingDate {get;set;}

        [DataType(DataType.Date)]
        [Required]
        public DateTime? EndingDate {get;set;}
    }


}
