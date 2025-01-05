using System.ComponentModel.DataAnnotations;

namespace Football_Management.ViewModels.Account
{
    public class LoginViewModel
    {
        public int AccountId { get; set; }

        [Required(ErrorMessage = "Username or Email is required")]
        public string? UsernameOrEmail { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
