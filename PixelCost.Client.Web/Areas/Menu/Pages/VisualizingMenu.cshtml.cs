using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PixelCost.Client.Web.Models.upload;

namespace PixelCost.Client.Web.Areas.Pages
{
    public class VisualizingMenuModel : PageModel
    {

        [BindProperty(SupportsGet = true)]
        public string? DurationID {get;set;}
        public DurationDTO? SelectedDuration {get;set;}
        public ICollection<SelectListItem>DurationList{get;set;}

        [BindProperty(SupportsGet = true)]
        public string? SubDurationID {get;set;}
        public SubDurationDTO? SelectedSubDuration {get;set;}
        public ICollection<SelectListItem>? SubDurationList {get;set;}

        [BindProperty(SupportsGet = true)]
        public string? CategoryID {get;set;}
        public CategoryDTO? SelectedCategory {get;set;}
        public ICollection<SelectListItem>? CategoryList{get;set;}

        
        
        

        public VisualizingMenuModel()
        {   
            // Retrieve from api using claim user id to create the list of duration
            DurationList = new List<SelectListItem>(){
                new ("Unselect item",string.Empty,true),
                new("Month 01","Month01",false),
                new("Month 02","Month02",false),
                new("Month 03","Month03",false),
                new("Month 04","Month04",false),
            };
             
        }

        public void OnGet()
        {
            
            // Initiate value for each section
            
            if(!string.IsNullOrEmpty(DurationID)){
                
                // Retrieve data from the API

               

                // 3.Retrieve all subDuration in this selected duration to make select list for the subDuration
               

            }


        }

    } 
}
