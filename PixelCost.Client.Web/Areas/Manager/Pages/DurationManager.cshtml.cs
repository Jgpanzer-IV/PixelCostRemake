using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace PixelCost.Client.Web.Areas.Pages
{
    public class DurationManagerModel : PageModel
    {
        
        [BindProperty(SupportsGet = true)]
        public string durationID {get;set;} = string.Empty;

        [BindProperty]
        [FromForm]
        public Duration Duration {get;set;} = new();

        public IActionResult OnGet()
        {   
            // if Duration id in null or empty string, So redirec to collection page 
            if (string.IsNullOrEmpty(durationID))
                return RedirectToPage("CollectionMenu",new {area = "Menu"}); 
            else
                return Page();

        }

        public void OnPost(){

        }
        
    }

    public class Duration{

        [BindProperty]
        [Required]
        public string? DurationName {get;set;}
        
        [BindProperty]
        [Required]
        public string? DurationCost {get;set;}

        [BindProperty]
        [Required]
        [DataType(DataType.Date)]
        public DateTime? StartingDate {get;set;}

        [BindProperty]
        [Required]
        [DataType(DataType.Date)]
        public DateTime? EndingDate {get;set;}

    }

}
