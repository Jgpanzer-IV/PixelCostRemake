using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PixelCost.Client.Web.Models.content;
using System.ComponentModel.DataAnnotations;

namespace PixelCost.Client.Web.Areas.Pages
{
    public class ExpenseManagerModel : PageModel
    {
        
        [BindProperty(SupportsGet = true)]
        public string? DurationID{get;set;}
        public IEnumerable<SelectListItem>? DurationList {get;set;}


        [BindProperty(SupportsGet = true)]
        public string? SubDurationID {get;set;}
        public IEnumerable<SelectListItem>? SubDurationList {get;set;}


        [BindProperty(SupportsGet = true)]
        public string? CategoryID {get;set;}
        public IEnumerable<SelectListItem>? CategoryList {get;set;}
        public IEnumerable<FinancialStatus>? CategoryFinancialInfo {get;set;}


        public IEnumerable<ListItem>? ExpenseList {get;set;}
        public IEnumerable<SelectListItem>? PaymentOptionList {get;set;}

        [BindProperty]
        public Expense Expense {get;set;} = new();

        public void OnGet()
        {

            
            DurationList = new List<SelectListItem>(){
                new("Select duration to manage.",string.Empty),
                new("Month 01","Month01"),
                new("Month 02","Month02"),
                new("Month 03","Month03")
            };

            // Retrieve Payment option available in the user

            PaymentOptionList = new List<SelectListItem>(){
                new ("Mobile Banking","Mobile Banking"),
                new ("Truemoney Wallet","Truemoney Wallet"),
                new ("Case","Case")
            };

            if(!string.IsNullOrEmpty(DurationID)){

                // Retrieve Sub duration list from the DurationID

                SubDurationList = new List<SelectListItem>(){
                    new("Select Sub duration to control.",string.Empty),
                    new("Week 01","Week01"),
                    new("Week 02","Week02"),
                    new("Week 03","Week03"),
                    new("Week 04","Week04")
                };
            }

            if(!string.IsNullOrEmpty(SubDurationID)){

                // Retrieve all category in the SubDuration using id reference

                CategoryList = new List<SelectListItem>(){
                    new("Food","Food"),
                    new("Material Raw","Material Raw"),
                    new("Stuff","Stuff"),
                    new("Cat Food","CatFood")
                };
            }
            
            if (!string.IsNullOrEmpty(CategoryID)){

                // Retrieve all expenses in the API using CategoryID reference

                ExpenseList = new List<ListItem>(){
                    new(){
                        Name = "Hamberger",
                        ValueElement = "670",
                        Info = DateTime.UtcNow.ToLongDateString(),
                        Description = "True money wallet"
                    },
                    new(){
                        Name = "Hot Dog",
                        ValueElement = "310",
                        Info = DateTime.UtcNow.ToLongDateString(),
                        Description = "Mobile Banking"
                    },new(){
                        Name = "KFC",
                        ValueElement = "670",
                        Info = DateTime.UtcNow.ToLongDateString(),
                        Description = "Cash"
                    },new(){
                        Name = "Chocolate",
                        ValueElement = "105",
                        Info = DateTime.UtcNow.Subtract(TimeSpan.FromDays(3)).ToLongDateString(),
                        Description = "True money wallet"
                    }
                    
                };

                CategoryFinancialInfo = new List<FinancialStatus>(){
                    new FinancialStatus("Cost","6,250",0),
                    new FinancialStatus("Balance","1,352",1),
                    new FinancialStatus("Expense","12,400",2),
                    new FinancialStatus("Average Expense","740",3),
                };
            }

        }
    }


    public class Expense{

        [BindProperty]
        [Required]
        public string? PaymentOption{get;set;}

        [BindProperty]
        [Required(ErrorMessage = "Please enter name of the recorded item")]
        public string? Name {get;set;}

        [BindProperty]
        [Required(ErrorMessage = "Please enter price of the recorded item")]
        public string? Price {get;set;}
        
        [BindProperty]
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime? RecordingDate {get;set;}

    }


}


