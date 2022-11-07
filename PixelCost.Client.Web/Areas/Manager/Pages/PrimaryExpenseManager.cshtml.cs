using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PixelCost.Client.Web.Models.content;
using PixelCost.Client.Web.Models.upload;
using PixelCost.Client.Web.Services.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Security.Policy;

namespace PixelCost.Client.Web.Areas.Pages
{
    [Authorize]
    public class PrimaryExpenseManager : PageModel
    {

        [BindProperty(SupportsGet = true)]
        public long? DurationId { get; set; }

        [BindProperty]
        public PrimaryExpenseForm PrimaryExpenseForm { get; set; }

        public IList<SelectListItem> DurationList { get; set; }
        public IList<SelectListItem> PaymentOptionList { get; set; }
        public IList<ListItem>? PrimaryExpenseList { get; set; }

        public DurationDTO? SelectedDuration { get; set; }

        private readonly ICommunicationServices _communicationServices;

        public PrimaryExpenseManager(ICommunicationServices communicationServices)
        {
            _communicationServices = communicationServices;
            DurationList = new List<SelectListItem>()
            {
                new SelectListItem("Select Duration","",true)
            };

            PrimaryExpenseList = new List<ListItem>();

            PaymentOptionList = new List<SelectListItem>();

            PrimaryExpenseForm = new();
        }

        public async Task<IActionResult> OnGet()
        {
            string errorMessage = string.Empty;

            // Check for selected duration.
            if (DurationId != null) {

                // retrieve for selected duration using id.
                Tuple<DurationDTO?, ProblemDetails?, HttpStatusCode> result = await _communicationServices.RetrieveEntityById<DurationDTO>("id/", DurationId??0, "", Constant.durationApi);

                if (result.Item3 == HttpStatusCode.OK)
                {
                    SelectedDuration = result.Item1;
                    SelectedDuration?.PrimaryExpenses?.ToList().ForEach(e => {
                        PrimaryExpenseList!.Add(new ListItem(e));
                    });

                }
                else if (result.Item3 == HttpStatusCode.NotFound)
                {
                    errorMessage += "The specified durationId is not found.\n";
                }
            }

            // Retrieve for all duration of the user and PaymentMethod
            string? userId = HttpContext.User?.Claims?.FirstOrDefault(e => e.Type == "sub")?.Value;

            if (!string.IsNullOrEmpty(userId)) {

                Tuple<List<DurationDTO>?, ProblemDetails?, HttpStatusCode> result = 
                    await _communicationServices.RetrieveEntityByClaimId<List<DurationDTO>?>("userId/", userId, "", Constant.durationApi);

                Tuple<IList<PaymentMethodDTO>?, ProblemDetails?, HttpStatusCode> paymentResult = 
                    await _communicationServices.RetrieveEntityByClaimId<IList<PaymentMethodDTO>?>(Constant.payment_RetrieveByUserId, userId, "", Constant.paymentMethodApi);

                // Checking result for retrieving Duration
                if (result.Item3 == HttpStatusCode.OK)
                {
                    result.Item1?.ForEach(e => {
                        DurationList.Add(new SelectListItem(e.Name, e.Id.ToString()));
                    });
                }
                else if (result.Item3 == HttpStatusCode.NotFound)
                {
                    errorMessage += "\n There is no duration yet, Please create a new one before recording primary expense.";
                }
                else {
                    errorMessage += "\n An error has occurred while retrieving data from DurationAPI, we're fixing as soon as possible.";
                }

                // Checking result for retrieving paymentMethod
                if (paymentResult.Item3 == HttpStatusCode.OK) {
                    paymentResult?.Item1?.ToList().ForEach(e => {
                        PaymentOptionList.Add(new SelectListItem(e.PaymentName, e.Id.ToString()));
                    });
                } else if (paymentResult.Item3 == HttpStatusCode.NotFound) {
                    errorMessage += "\n There is no payment option for retrieve, Please go to create a new one at Payment Manager section.";
                }
                else {
                    errorMessage += "\n The Payment api server fail to retrieve, We'll are fixing as soon as possible.";
                }
                if (!string.IsNullOrEmpty(errorMessage))
                    ViewData["problem"] = errorMessage;
                return Page();

            }
            return RedirectToPage("Index");
        }

        public async Task<IActionResult> OnPostCreate()
        {


            if (ModelState.IsValid)
            {

                // Retrieve for all duration of the user
                string? userId = HttpContext.User?.Claims?.FirstOrDefault(e => e.Type == "sub")?.Value;

                PrimaryExpenseDTO primaryExpenseDTO = new PrimaryExpenseDTO()
                {
                    OrderingName = PrimaryExpenseForm.Name ?? string.Empty,
                    OrderingPrice = PrimaryExpenseForm.Price,
                    DurationId = PrimaryExpenseForm.DurationId,
                    OrderingDate = PrimaryExpenseForm.OrderingDate ?? DateTime.Now,
                    UserId = userId ?? string.Empty,
                    PaymentMethodId = PrimaryExpenseForm.PaymentId
                };
                Tuple<PrimaryExpenseDTO?, ProblemDetails?, HttpStatusCode> result = await _communicationServices.PostEntityByObject("", primaryExpenseDTO, "", Constant.primaryExpenseApi);

                if (result.Item3 == HttpStatusCode.Created)
                {
                    return RedirectToPagePermanent("PrimaryExpenseManager", new { area = "Manager", DurationId = primaryExpenseDTO.DurationId});
                }
                else if (result.Item3 == HttpStatusCode.BadRequest)
                {
                    ViewData["problem"] = result.Item2?.Title + result.Item2?.Detail;
                    return Page();
                }
                else if (result.Item3 == HttpStatusCode.InternalServerError)
                {
                    ViewData["problem"] = "There are error internal the Server API, We're fixing as soon as possible.";
                    return Page();
                }
                else
                {
                    ViewData["problem"] = "Unidentify problem has occurred, Please connect admin.";
                    return Page();
                }

            }
            else {
                ViewData["problem"] = "Format is incorrect, Plase specify the correct format.";
                return Page();
            }
        }

        public IActionResult OnPostUpdate() {
            if (DurationId == null)
            {
                ViewData["problem"] = "Please select Duration before recording primary expense.";
                return Page();
            }
            return Page();
        }
    }


    public class PrimaryExpenseForm {

        [Required(ErrorMessage = "Please enter Primary expense Name.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Please specify Primary expense Price.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Please specify the Ordering Date.")]
        [DataType(DataType.DateTime)]
        public DateTime? OrderingDate { get; set; }

        [Required(ErrorMessage = "Please select payment method.")]
        public long PaymentId { get; set; }

        [Required]
        public long DurationId { get; set; }
    }

}
