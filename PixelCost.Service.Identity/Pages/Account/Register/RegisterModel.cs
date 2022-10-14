using System.ComponentModel.DataAnnotations;

namespace PixelCost.Identity.Pages.Register
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Please input your Username used in this service.")]
        [Display(Name = "Username")]
        [RegularExpression("^[A-Za-z0-9]+$")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please input email address to identify unique identity.")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare(nameof(Password))]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Job Title")]
        [StringLength(maximumLength: 32)]
        public string Jobtitle { get; set; }

        [Required]
        [Display(Name = "Initial cost")]
        [DataType(DataType.Currency)]
        public decimal InitialCost { get; set; }


    }
}
