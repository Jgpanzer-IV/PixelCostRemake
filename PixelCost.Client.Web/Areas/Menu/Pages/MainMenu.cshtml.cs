using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PixelCost.Client.Web.Models.upload;
using PixelCost.Client.Web.Services.Interfaces;
using System.Net;

namespace PixelCost.Client.Web.Areas.Pages
{

    [Authorize]
    public class MainMenuModel : PageModel
    {
        
        private ICommunicationServices _communicationServices;

        public MainMenuModel(ICommunicationServices communicationServices)
        {
            _communicationServices = communicationServices;
        }
        
        public WalletDTO? walletDTO { get; set; }

        public async Task<PageResult> OnGet()
        {

            // Retrieve 'Wallet' entity from the API using claim id in the identity token
            string? userId = HttpContext.User?.Claims?.FirstOrDefault(e => e.Type == "sub")?.Value;

            if (userId == null) { 
                ViewData["unExspectedProblem"] = "The user is not authorized.";
                return Page();
            }

            Tuple<WalletDTO?,ProblemDetails?,HttpStatusCode> result = await _communicationServices.RetrieveEntityByClaimId<WalletDTO>("",userId,"",Constant.walletUserApi);

            // It indicate that the request was success if it true.
            if (result.Item1 != null)
                walletDTO = result.Item1;
            else if (result.Item3 == HttpStatusCode.BadRequest)
                ViewData["problem"] = result.Item2;
            else if (result.Item3 == HttpStatusCode.NotFound)
                ViewData["problem"] = "Not found the entity for the specified userId.";
            else if (result.Item3 == HttpStatusCode.InternalServerError)
                ViewData["problem"] = "Cannot retrieve data entity from the server, Because There is something went wrong internal the server.";
            else
                ViewData["problem"] = "There are unidentify errors about retrieving data entity.";

            return Page();
        }
    }
}
