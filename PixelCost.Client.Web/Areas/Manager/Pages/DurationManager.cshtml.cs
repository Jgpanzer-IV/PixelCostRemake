using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PixelCost.Client.Web.Models.upload;
using PixelCost.Client.Web.Services.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace PixelCost.Client.Web.Areas.Pages
{
    public class DurationManagerModel : PageModel
    {
        
        [BindProperty(SupportsGet = true)]
        public long DurationID {get;set;}

        [BindProperty]
        [FromForm]
        public DurationForm Duration {get;set;}

        public DurationDTO? RetrievedDuration { get;set;}

        private readonly ICommunicationServices _communicationServices;

        public DurationManagerModel(ICommunicationServices communicationServices)
        {
            Duration = new();
            _communicationServices = communicationServices;
        }

        public async Task<IActionResult> OnGet()
        {   
            Tuple<DurationDTO?, ProblemDetails?, HttpStatusCode> result = await _communicationServices.RetrieveEntityById<DurationDTO>("id/",DurationID,"",Constant.durationApi);

            if (result.Item3 == HttpStatusCode.OK)
            {
                RetrievedDuration = result.Item1;
                Duration.DurationName = result.Item1?.Name;
                Duration.DurationCost = result.Item1?.InitialCost.ToString();
                Duration.StartingDate = result.Item1?.StartingDate;
                Duration.EndingDate = result.Item1?.EndingDate;

                return Page();
            }
            else if (result.Item3 == HttpStatusCode.NotFound)
            {
                ViewData["problem"] = "There is no specified Id for duration to be retrieved.";
                //return RedirectToPagePermanent("CollectionMenu", new { area = "Menu" });
                return Page();
            }
            else {
                ViewData["problem"] = "There is unexspected problem, Please contact admin at the following link.";
                return Page();
            }

        }
        public async Task<IActionResult> OnPostUpdateAsync() {

            // Prevent invalid reqeust.
            if (!ModelState.IsValid)
                return RedirectToPagePermanent("CollectionMenu", new { area = "Menu" });
            
            string? userId = HttpContext.User.Claims?.FirstOrDefault(e => e.Type == "sub")?.Value;

            if (string.IsNullOrEmpty(userId))
                return RedirectToPagePermanent("MainMenu", new { area = "Menu" });


            DurationDTO updatedEntity = new() {
                Id = DurationID,
                UserId = userId,
                Name = Duration.DurationName ?? string.Empty,
                InitialCost = Convert.ToDecimal(Duration.DurationCost??"0"),
                StartingDate = Duration?.StartingDate ?? DateTime.Today,
                EndingDate = Duration?.EndingDate ?? DateTime.Today,
                IsActive = true,
            };

            Tuple<DurationDTO?, ProblemDetails?, HttpStatusCode> result = await _communicationServices.PatchEntityByObject<DurationDTO?>("", DurationID, updatedEntity, "", Constant.durationApi);

            if (result.Item3 == HttpStatusCode.Accepted)
            {
                RetrievedDuration = result.Item1;
                return Page();
            }
            else if (result.Item3 == HttpStatusCode.NotFound)
            {
                ViewData["problem"] = "There is no entity to be updated.";
                return RedirectToPagePermanent("CollectionMenu", new { area = "Menu" });
            }
            else if (result.Item3 == HttpStatusCode.BadRequest)
            {
                ViewData["problem"] = result.Item2?.Title + ", " + result.Item2?.Detail;
                return Page();
            }
            else {
                ViewData["problem"] = "There is a problem internal the server, We're fixing it as soon as possible.";
                return Page();
            }

        }

        public async Task<IActionResult> OnPostDeleteAsync() {

            // Prevent invalid reqeust.
            if(!ModelState.IsValid)
                return RedirectToPage("CollectionMenu", new { area = "Menu" });

            // Send request to delete the entity
            Tuple<bool, HttpStatusCode> result = await _communicationServices.DeleteEntityById("",DurationID,"",Constant.durationApi);

            if (result.Item2 == HttpStatusCode.NoContent)
            {
                return RedirectToPagePermanent("CollectionMenu", new { area = "Menu" });
            }
            else if (result.Item2 == HttpStatusCode.NotFound)
            {
                ViewData["problem"] = "There is no entity to be deleted.";
                return Page();
            }
            else {
                ViewData["problem"] = "There is an error internal the server, we're fixing it as soon as possible.";
                return Page();
            }



        }

        
    }

    public class DurationForm{

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
