using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using PixelCost.Service.Identity.Models;
using Microsoft.AspNetCore.Authorization;
using System.Net.Mail;
using System.Net;
using PixelCost.Service.Model.DTOModel;
using System.Text.Json;
using System.Text;

namespace PixelCost.Identity.Pages.Register
{

    [AllowAnonymous]
    public class Index : PageModel
    {

        [BindProperty]
        public RegisterModel registerModel { get; set; } = new();

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;

        public Index(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration,
            IHttpClientFactory httpClientFactory)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public void OnGet()
        { }

        public RedirectResult OnPostLogin() {
            return RedirectPermanent(_configuration["UriServer:Interactive.Client"] +"/Menu/MainMenu");
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid) {
                return Page();
            }

            ApplicationUser newUser = new()
            {
                UserName = registerModel.UserName,
                Email = registerModel.Email,
            };

            var result = await _userManager.CreateAsync(newUser, registerModel.Password);

            if (result.Succeeded)
            {
                WalletDTO walletDTO = new() {
                    UserID = newUser.Id,
                    Username = newUser.UserName,
                    JobTitle = registerModel.Jobtitle,
                    Balance = registerModel.InitialCost,
                    TotalNumberExpense = 0,
                    TotalNumberRevenue = 0,
                    DateCreate = DateTime.Now
                };

                string content = JsonSerializer.Serialize(walletDTO);

                StringContent stringContent = new StringContent(content, Encoding.UTF8, "application/json");

                HttpClient httpClient = _httpClientFactory.CreateClient();

                HttpResponseMessage response = await httpClient.PostAsync(_configuration["UriServer:Service.WalletAPI"] + _configuration["RelativePath:WalletEndpoint"], stringContent);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToPagePermanent("/Account/EmailConfirmation/Index", pageHandler: "Send", new { email = newUser.Email });
                }
                else if (response.StatusCode == HttpStatusCode.BadRequest) { 
                    ProblemDetails problemDetails = await response.Content.ReadFromJsonAsync<ProblemDetails>();
                    ViewData["UnExspectedProblem"] = problemDetails.Title + problemDetails.Detail;
                    return Page();
                }
                else
                {
                    ViewData["UnExspectedProblem"] = "The server cannot create the posted entity, Please contect admin.";
                    return Page();
                }
            }
            else 
            {
                foreach (var error in result.Errors) {
                    ModelState.AddModelError(error.Code, error.Description);
                }
                return Page();
            }

        }


       

    }
}
