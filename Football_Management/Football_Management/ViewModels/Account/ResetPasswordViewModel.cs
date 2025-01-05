using System.ComponentModel.DataAnnotations;

namespace Football_Management.ViewModels.Account
{
    public class ResetPasswordViewModel
    {
        [Required]
        public string? Token { get; set; }

        [Required(ErrorMessage = "New password is required.")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters.")]
        public string? NewPassword { get; set; }

        [Required(ErrorMessage = "Confirming the new password is required.")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation new password do not match.")]
        public string? ConfirmNewPassword { get; set; }
    }
}
