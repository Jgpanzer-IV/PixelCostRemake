using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using PixelCost.Client.Web.Services.Interfaces;
using System.Net;
using PixelCost.Client.Web.Models.upload;

namespace PixelCost.Client.Web.Areas.Pages
{
    public class WalletCreatorModel : PageModel
    {

        [BindProperty]
        public PaymentForm paymentForm { get; set; }

        [BindProperty(SupportsGet = true)]
        public long? id{get; set;}

        public IEnumerable<SelectListItem>? paymentTypeList {get;set;} 

        private readonly ICommunicationServices _communicationServices;

        public WalletCreatorModel(ICommunicationServices communicationServices)
        {
            _communicationServices = communicationServices;

            paymentTypeList = new List<SelectListItem>(){
                new("Digital","Digital",false),
                new("Cash","Cash",false)
            };

            paymentForm = new();
        }

        public async Task OnGet()
        {
            // Check it wheather update entity or create new entity
            if (id.HasValue || id != null) { 
                string userId = HttpContext.User?.FindFirst("sub")?.Value ?? "";
                if (userId != null) {

                    Tuple<PaymentMethodDTO?, ProblemDetails?, HttpStatusCode> result = await _communicationServices.RetrieveEntityById<PaymentMethodDTO>(Constant.payment_RetrieveById,id??0,"",Constant.paymentMethodApi);
                    if (result.Item3 == HttpStatusCode.OK)
                    {
                        paymentForm = new PaymentForm() { 
                            Id = result.Item1?.Id ?? 0,
                            Name = result.Item1?.PaymentName ?? "",
                            Type = result.Item1?.PaymentType ?? ""
                        };
                    }
                    else {
                        id = null;
                    } 
                }
            }
        }

        public async Task<IActionResult> OnPostCreate(){
            // Perfrom operation here

            if (!ModelState.IsValid)
                return Page();

            string? userId = HttpContext.User?.Claims?.FirstOrDefault(e => e.Type == "sub")?.Value;
            if (string.IsNullOrEmpty(userId))
                return RedirectToPagePermanent("Index");

            PaymentMethodDTO paymentMethodDTO = new() {
                UserId = userId,    
                PaymentName = paymentForm.Name,
                PaymentType = paymentForm.Type,
                PaymentExpenseCount = 0,
                PaymentRevenueCount = 0,
                Symbol = "/images/icon/payments/"+ paymentForm.Type.ToLower() +".png",
                DateCreate = DateTime.Now
            };

            // Post to the Payment API
            Tuple<PaymentMethodDTO?, ProblemDetails?, HttpStatusCode> result = await _communicationServices.PostEntityByObject<PaymentMethodDTO>("", paymentMethodDTO, "", Constant.paymentMethodApi);

            return ValidateReturn(result.Item3, result.Item2);
        }


        public async Task<IActionResult> OnPostUpdate() {

            if (!ModelState.IsValid)
                return Page();

            string? userId = HttpContext.User?.Claims?.FirstOrDefault(e => e.Type == "sub")?.Value;
            if (string.IsNullOrEmpty(userId))
                return RedirectToPagePermanent("Index");

            // Retrieve for the entity to be updated.
            Tuple<PaymentMethodDTO?, ProblemDetails?, HttpStatusCode> result = await _communicationServices.RetrieveEntityById<PaymentMethodDTO>(Constant.payment_RetrieveById, paymentForm.Id, "", Constant.paymentMethodApi);

            if (result.Item3 == HttpStatusCode.OK)
            {
                if (result.Item1 != null)
                {
                    PaymentMethodDTO existsEntity = result.Item1;

                    // Update entity here
                    existsEntity.PaymentName = paymentForm.Name;
                    existsEntity.PaymentType = paymentForm.Type;
                    existsEntity.Symbol = "/images/icon/payments/" + paymentForm.Type.ToLower() + ".png";

                    // Push the updated to the api server

                    Tuple<PaymentMethodDTO?, ProblemDetails?, HttpStatusCode> updatedResult = await _communicationServices.PatchEntityByObject<PaymentMethodDTO>("",existsEntity.Id,existsEntity, "", Constant.paymentMethodApi);

                    return ValidateReturn(updatedResult.Item3, updatedResult.Item2);
                }
                else
                {
                    ViewData["problem"] = "There is an error ocurred while retrieving data entity for updating.";
                    return Page();
                }
            }

            return ValidateReturn(result.Item3,result.Item2);

        }


        public async Task<IActionResult> OnGetDelete() {

            Tuple<bool, HttpStatusCode> result = await _communicationServices.DeleteEntityById("", id ?? 0, "",Constant.paymentMethodApi);

            return ValidateReturn(result.Item2, null);
        }


        private IActionResult ValidateReturn(HttpStatusCode statusCode, ProblemDetails? problem) {
            if (statusCode == HttpStatusCode.Created || statusCode == HttpStatusCode.NoContent)
            {
                return RedirectToPagePermanent("PaymentManager", new { area = "Manager" });
            }
            else if (statusCode == HttpStatusCode.NotFound) {
                ViewData["problem"] = "Not found the content to be delete.";
                return Page();
            }
            else if (statusCode == HttpStatusCode.BadRequest)
            {
                ViewData["problem"] = problem?.Title + problem?.Detail;
                return Page();
            }
            else if (statusCode == HttpStatusCode.InternalServerError)
            {
                ViewData["problem"] = "Cannot perform CRUD operation in the server PaymentMerhod, Because There are something went wrong internal the server. We are fixing as soon as possible.";
                return Page();
            }
            else
            {
                ViewData["problem"] = "Cannot identify the error that ocurred while posting the new entity.";
                return Page();
            }


        }

    }

    public class PaymentForm {

        public long Id { get; set; }

        [Required(ErrorMessage = "Plase enter the name of wallet.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please specify the type of the wallet")]
        public string Type { get; set; } = string.Empty;

    }

}
