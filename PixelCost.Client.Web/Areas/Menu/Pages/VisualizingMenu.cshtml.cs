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

                SelectedDuration = new(){
                    Name = "Month 01",
                    StartingDate = new DateTime(2022,7,1),
                    EndingDate = new DateTime(2022,7,31),
                    RemainDate = new DateTime(2022,7,31).Subtract(DateTime.Now).Days,
                    Progress = (byte) ((DateTime.Now.Day * 100) / new DateTime(2022,7,31).Day),
                    Cost = 58000m,
                    CostSubDuration = 40000m, // used 35000
                    Expense = 32000m,
                    AverageExpense = 32000 / 31,
                    Balance =  3000 + 5000, // Sum Balance of sub duration + Untouched
                    Untouched = 5000,
                    IsActive = true,
                    PrimaryExpenses = new(){
                        new(){
                            Name = "Room",
                            Price = 6000,
                            OrderDate = new DateTime(2022,07,02),
                            PaymentMethodID = "Krungthai van"
                        },
                        new(){
                            Name = "Service",
                            Price = 2000,
                            OrderDate = new DateTime(2022,07,02),
                            PaymentMethodID = "True money wallet"
                        },
                        new(){
                            Name = "Network",
                            Price = 1500,
                            OrderDate = new DateTime(2022,07,04),
                            PaymentMethodID = "Krungthai van"
                        },
                        new(){
                            Name = "Benze",
                            Price = 8500,
                            OrderDate = new DateTime(2022,07,09),
                            PaymentMethodID = "Cash"
                        },
                    },
                    SubDurations = new(){
                        new(){
                            Name = "Week 01",
                            StartingDate = new DateTime(2022,7,1),
                            EndingDate = new DateTime(2022,7,7),
                            RemainDate = 0,
                            Progress = 100,
                            Cost = 17500,
                            CostCategory = 17500,
                            Expense = 16000,
                            AverageExpense = 16000 / 7,
                            Balance = 1500,
                            Categories = new(){
                                new (){
                                    Name = "Food",
                                    Cost = 5000,
                                    Expense = 4500, 
                                    AverageExpense = 4500 / 7,
                                    Balance = 500,
                                    ExpenseOrders = new(){
                                        new(){
                                            Name = "Sealmon",
                                            OrderDate = new DateTime(2022,7,4),
                                            OrderPrice = 800,
                                            PaymentID = "Krungthai van"
                                        },
                                        new(){
                                            Name = "Chicken",
                                            OrderDate = new DateTime(2022,7,5),
                                            OrderPrice = 200,
                                            PaymentID = "Krungthai van"
                                        },
                                        new(){
                                            Name = "Stuff",
                                            OrderDate = new DateTime(2022,7,6),
                                            OrderPrice = 2000,
                                            PaymentID = "True money wallet"
                                        },
                                        new(){
                                            Name = "Buffei",
                                            OrderDate = new DateTime(2022,7,4),
                                            OrderPrice = 1500,
                                            PaymentID = "Krungthai van"
                                        }
                                       
                                    }
                                }
                            }
                        }
                    },
                
                };

                // 3.Retrieve all subDuration in this selected duration to make select list for subduration
                SubDurationList = new List<SelectListItem>();
                SubDurationList.Add(new("Unselect item",string.Empty,true));
                SelectedDuration.SubDurations.ForEach(e =>{
                    SubDurationList.Add(new(e.Name,e.Name,false));
                });

                if(!string.IsNullOrEmpty(SubDurationID)){
                    // Retrieve from api
                    SelectedSubDuration = SelectedDuration?.SubDurations?.FirstOrDefault();

                    CategoryList = new List<SelectListItem>();
                    CategoryList.Add(new("Unselect item",string.Empty,true));
                    SelectedSubDuration?.Categories?.ForEach(e => {
                        CategoryList.Add(new(e.Name,e.Name,false));
                    });
                
                    if(!string.IsNullOrEmpty(CategoryID)){
                        
                        // Retrieve category from api
                        SelectedCategory = SelectedSubDuration?.Categories?.FirstOrDefault();
                    }
                }        
            }


        }

    } 
}
