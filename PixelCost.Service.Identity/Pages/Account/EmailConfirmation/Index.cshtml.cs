using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PixelCost.Service.Identity.Models;
using Microsoft.AspNetCore.Authorization;
using System.Net.Mail;
using System.Net;

namespace PixelCost.Service.Identity.Pages.EmailConfirmation
{
    [AllowAnonymous]
    public class IndexModel : PageModel
    {
        [BindProperty]
        public string Email { get; set; }

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public IndexModel(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public void OnGet(string email)
        {
            ViewData["Message"] = "We’ve sent an email confirmation link to your registered email, Please check in your inbox or junk message and click the link to confirm the email.";
            ViewData["Email"] = email;
        }


        public async Task<IActionResult> OnGetValidateTokenAsync(string email, string token) {

            var user = await _userManager.FindByEmailAsync(email);

            if (user != null) { 
            
                var result = await _userManager.ConfirmEmailAsync(user, token);

                if (result.Succeeded)
                {
                    return RedirectPermanent(_configuration.GetSection("UriServer")["Interactive.Client"]+"/Menu/MainMenu");
                }
                else 
                {
                    ViewData["Message"] = "Error has occured to the rusult of Confirmation user using token";
                    return Page();
                }
            }
            ViewData["Message"] = "There is no specified user's email";
            return Page();
                    
        }

        public RedirectResult OnGetLogin() {
            return RedirectPermanent(_configuration["UriServer:Client.Web"]+"/Menu/MainMenu");
        }

        public async Task<RedirectToPageResult> OnGetSendAsync(string email)
        {

            var user = await _userManager.FindByEmailAsync(email);

            if (user != null)
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var urlConfirmation = Url.PageLink("/Account/EmailConfirmation/Index", pageHandler: "ValidateToken", values: new { email = email, token = token });

                // Send confirmation link to the user's email.
                var mailMessage = new MailMessage(
                    _configuration["EmailBroker:UserName"],
                    email,
                    "Please confirm your email againt PixelSchme services",
                    $"Thank you for using our services, Please click the confirmation link here to complete sign up \n{urlConfirmation}");

                using (var emailClient = new SmtpClient(_configuration["EmailBroker:Url"], int.Parse(_configuration["EmailBroker:Port"].ToString())))
                {

                    // Autorize sender using network credential in SMTP server
                    emailClient.Credentials = new NetworkCredential(
                        _configuration["EmailBroker:UserName"],
                        _configuration["EmailBroker:Password"]);

                    await emailClient.SendMailAsync(mailMessage);

                }

                return RedirectToPagePermanent("/Account/EmailConfirmation/Index", new { email = email });
            }
            else
            {
                return RedirectToPagePermanent("/Account/EmailConfirmation/InvalidEmail");
            }
        }

        public RedirectToPageResult OnPost() {
            return RedirectToPagePermanent("/Account/EmailConfirmation/Index",pageHandler:"Send",new {email = Email});
        }
    
    
    }
}
