using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PixelCost.Client.Web.Models.upload;
using PixelCost.Client.Web.Services.Interfaces;
using System.Net;

namespace PixelCost.Client.Web.Areas.Pages
{

    [Authorize]
    public class WalletManagerModel : PageModel
    {   

        private readonly ICommunicationServices _communicationServices;

        [BindProperty(SupportsGet = true)]
        public long? SelectedPaymentId {get;set;}
        [BindProperty(SupportsGet = true)]
        public decimal? amount { get; set; }
        public ICollection<SelectListItem> PaymentList { get; set; } = new List<SelectListItem>();
        public PaymentMethodDTO? SelectedPayment {get;set;}

        public WalletManagerModel(ICommunicationServices communicationServices)
        {
            _communicationServices = communicationServices;
            PaymentList.Add(new SelectListItem
            {
                Text = "Select payment name",
                Value = string.Empty,
                Selected = true
            });
        }

        public async Task<IActionResult> OnGetAsync()
        {
            string? userId = HttpContext.User?.Claims?.FirstOrDefault(e => e.Type == "sub")?.Value;
            if (!string.IsNullOrEmpty(userId))
            {

                // Retrieve entity for selected payment 
                if (SelectedPaymentId.HasValue) {
                    long id = SelectedPaymentId ?? 0;
                    Tuple<PaymentMethodDTO?, ProblemDetails?, HttpStatusCode> resultSelected = 
                        await _communicationServices.RetrieveEntityById<PaymentMethodDTO>(Constant.payment_RetrieveById,id,"",Constant.paymentMethodApi);

                    if (resultSelected.Item3 == HttpStatusCode.OK)
                        SelectedPayment = resultSelected.Item1;
                }

                // Retrieve entities for all payment option for the user
                Tuple<List<PaymentMethodDTO>?,ProblemDetails?, HttpStatusCode> result = await _communicationServices.RetrieveEntityByClaimId<List<PaymentMethodDTO>>(Constant.payment_RetrieveByUserId, userId, "", Constant.paymentMethodApi);
                if (result.Item1 != null)
                {
                    List<PaymentMethodDTO> paymentMethodDTOs = result.Item1;

                    paymentMethodDTOs.ForEach(e =>
                    {
                        PaymentList.Add(new SelectListItem
                        {
                            Text = e.PaymentName,
                            Value = Convert.ToString(e.Id),
                            Selected = false
                        });
                    });
                    return Page();
                }
                else if (result.Item3 == HttpStatusCode.NotFound)
                {
                    ViewData["problem"] = "There is no Payment method yet, Please Create new at the button.";
                    return Page();
                }
                else if (result.Item3 == HttpStatusCode.InternalServerError)
                {
                    ViewData["problem"] = "The Entity cannot retrieved, Because There is an error in the server. We're solving this problem as soon as possible.";
                    return Page();
                }
                else {
                    ViewData["problem"] = "Cannot identify the problem, We're solving this problem as soon as possible.";
                    return Page();
                }
            }
            else
                return RedirectToPagePermanent("Index");
        }

  

    



    }

   

}
