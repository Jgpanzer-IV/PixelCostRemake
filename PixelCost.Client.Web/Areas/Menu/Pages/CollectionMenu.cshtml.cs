using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PixelCost.Client.Web.Models.content;
using PixelCost.Client.Web.Models.upload;
using PixelCost.Client.Web.Services.Interfaces;
using System.Net;

namespace PixelCost.Client.Web.Areas.Pages
{
    [Authorize]
    public class CollectionMenuModel : PageModel
    {

        public IList<ProgressItem>? DurationList { get; set; }
        public IList<ProgressItem>? AchivedDurationList { get; set; }

        private readonly ICommunicationServices _communicationServices; 

        public CollectionMenuModel(ICommunicationServices communicationServices)
        {
            _communicationServices = communicationServices;
            DurationList = new List<ProgressItem>();
            AchivedDurationList = new List<ProgressItem>();
        }

        public async Task<PageResult> OnGet()
        {
            // Get user-id from claim type id.
            string? userId = HttpContext.User?.Claims?.FirstOrDefault(e => e.Type == "sub")?.Value;

            if (userId != null)
            {
                // Retrieve the duration entity based on the claim id.

                Tuple<List<DurationDTO>?, ProblemDetails?, HttpStatusCode> result = await _communicationServices.RetrieveEntityByClaimId<List<DurationDTO>>("userId/", userId, "", Constant.durationApi);

                if (result.Item3 == HttpStatusCode.OK)
                {
                    if (result.Item1 != null && DurationList != null && AchivedDurationList != null)
                    {
                        result.Item1.ForEach(action =>
                        {
                            if (action.IsActive ?? true)
                                DurationList.Add(new ProgressItem(action));
                            else
                                AchivedDurationList.Add(new ProgressItem(action));
                        });
                    }
                }
                else if (result.Item3 == HttpStatusCode.NotFound)
                {
                    ViewData["problem"] = "There is no duration yet.";
                }
                else
                {
                    ViewData["problem"] = "UnExpected problem has ocurred while retrieving data.";
                }
            }
            return Page();

        }
    }
}
