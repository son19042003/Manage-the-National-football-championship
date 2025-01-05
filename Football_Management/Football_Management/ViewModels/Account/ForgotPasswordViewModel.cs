using System.ComponentModel.DataAnnotations;

namespace Football_Management.ViewModels.Account
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string? Email { get; set; }
    }
}
