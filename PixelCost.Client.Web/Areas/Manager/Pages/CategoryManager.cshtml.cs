using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PixelCost.Client.Web.Models.content;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PixelCost.Client.Web.Areas.Pages
{
    public class CategoryManagerModel : PageModel
    {


        [BindProperty(SupportsGet = true)]
        public string? DurationID {get;set;}
        public List<SelectListItem>? DurationList {get;set;}
        public FullProgressBar? DurationProgress {get;set;}


        [BindProperty(SupportsGet = true)]
        public string? SubDurationID {get;set;}
        public List<SelectListItem>? SubDurationList{get;set;}
        public ShortProgressBar? SubDurationProgress {get;set;}
        public FinancialStatus? BalanceForCategory {get;set;}
        public List<ProgressItem>? CategoryItemList {get;set;}


        [BindProperty]
        public Category Category {get;set;} = new();

        public PageResult OnGet()
        {
            DurationList = new List<SelectListItem>(){
                new("Select duration to manage.",string.Empty),
                new("Month 01","Month01"),
                new("Month 02","Month02"),
                new("Month 03","Month03")
            };
            if (!string.IsNullOrEmpty(DurationID)){
                var currentDate = DateTime.Now;
                var startingDate = new DateTime(2022,07,01);
                var endingDate = new DateTime(2022,07,31);

                DurationProgress = new(){
                    Value01 = startingDate.ToShortDateString(),
                    Value02 = currentDate.ToShortDateString(),
                    Value03 = endingDate.ToShortDateString(),
                    Label01 = "Starting Date",
                    Label02 = "Current",
                    Label03 = "Ending Date",
                    Description = "Remain " + endingDate.Subtract(currentDate).Days.ToString() + " day will reach the end",
                    Progress = 35,
                    Information = "Pregress 35%"
                };

                SubDurationList = new(){
                    new("Select Sub duration to control.",string.Empty),
                    new("Week 01","Week01"),
                    new("Week 02","Week02"),
                    new("Week 03","Week03"),
                    new("Week 04","Week04")
                };

            }else{
                
            }

            if (!string.IsNullOrEmpty(SubDurationID)){
                SubDurationProgress = new(){
                    Label = "Progress 64%.",
                    Description = "Remain 4 day will reach the end.",
                    ValueProgress = 64
                };

                BalanceForCategory = new("Remain for Category","7,400",1);

                CategoryItemList = new(){
                    new(){
                        MainText = "Balance 74%",
                        InformationText = "Balance",
                        Value = "4670 B",
                        ItemName = "Food",
                        Progress = 74
                    },
                    new(){
                        MainText = "Balance 31%",
                        InformationText = "Balance",
                        Value = "2,350 B",
                        ItemName = "Stuff",
                        Progress = 31
                    },
                    new(){
                        MainText = "Balance 12%",
                        InformationText = "Balance",
                        Value = "560 B",
                        ItemName = "Material Raw",
                        Progress = 12
                    }
                };
            }
            return Page();
        }

        public void OnPost(){
        

        }
    }

    
    public class DurationOption{
        
    }

    public class Category{
        
        [BindProperty]
        [Required]
        public string? Name {get;set;}
        
        [BindProperty]
        [Required]
        public string? Cost {get;set;}

    }
}
