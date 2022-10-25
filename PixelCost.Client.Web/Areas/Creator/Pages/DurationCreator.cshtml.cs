using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Reflection.Metadata;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PixelCost.Client.Web.Models.upload;
using PixelCost.Client.Web.Services.Interfaces;

namespace PixelCost.Client.Web.Areas.Pages
{
    [Authorize]
    public class DurationCreatorModel : PageModel
    {

        [BindProperty]
        public DurationInfo duration {get;set;}

        private readonly ICommunicationServices _communicationServices;

        public DurationCreatorModel(ICommunicationServices communicationServices)
        {
            _communicationServices = communicationServices;
            duration = new();
        }

        public void OnGet()
        {}

        public async Task<IActionResult> OnPostAsync(){
            
            if (!ModelState.IsValid)
                return Page();

            string? userId = HttpContext.User?.Claims?.FirstOrDefault(e => e.Type == "sub")?.Value;

            if (userId != null) {

                DurationDTO postedEntity = new()
                {
                    UserId = userId,
                    Name = duration.Name ?? string.Empty,
                    InitialCost = duration.Cost ?? 0,
                    StartingDate = duration.StartingDate ?? DateTime.Now,
                    EndingDate = duration.EndingDate ?? DateTime.Now
                };
             
                Tuple<DurationDTO?,ProblemDetails?,HttpStatusCode> result = await _communicationServices.PostEntityByObject<DurationDTO?>("",postedEntity, "", Constant.durationApi);

                if (result.Item3 == HttpStatusCode.Created)
                {
                    return RedirectToPagePermanent("CollectionMenu", new { area = "Menu" });
                }
                else if (result.Item3 == HttpStatusCode.BadRequest)
                {
                    ViewData["problem"] = result.Item2?.Title + result.Item2?.Detail;
                    return Page();
                }
                else if (result.Item3 == HttpStatusCode.InternalServerError) {
                    ViewData["problem"] = "There is an error in the server API.";
                    return Page();
                }

            }
            return RedirectToPage("CollectionMenu",new {area = "Menu"});
        }
    }


    public class DurationInfo{

        [DisplayName("DurationForm's Name")]
        [Required(ErrorMessage = "Please enter name of the duration")]
        public string? Name {get;set;} = string.Empty;

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
