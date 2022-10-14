using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PixelCost.Client.Web.Areas.Pages
{
    public class DurationCreatorModel : PageModel
    {

        [BindProperty]
        public DurationInfo duration {get;set;} = new();

        public void OnGet()
        {
        }

        public IActionResult OnPostAsync(){
            
            if (!ModelState.IsValid)
                return Page();

            // Save data in api

            return RedirectToPage("CollectionMenu",new {area = "Menu"});
        }
    }


    public class DurationInfo{

        [DisplayName("Duration's Name")]
        [Required(ErrorMessage = "Please enter name of the duration")]
        [RegularExpression(@"^[a-z]+\s*$")]
        public string Name {get;set;} = string.Empty;

        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "Please specify cost of the duration")]
        [Range(0.0,double.MaxValue)]
        public decimal? Cost {get;set;} 

        [DisplayName("Starting date for the duration")]
        [Required(ErrorMessage = "Please specify the starting date")]
        [DataType(DataType.Date)]
        public DateTime? StartingDate {get;set;}
        
        [DisplayName("Ending date for the duration")]
        [Required(ErrorMessage = "Please specify the ending date")]
        [DataType(DataType.Date)]
        public DateTime? EndingDate {get;set;}

    }

}
