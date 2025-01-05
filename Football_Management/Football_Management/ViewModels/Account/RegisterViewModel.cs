using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Football_Management.ViewModels.Account
{
    public class RegisterViewModel
    {
        public int AccountId { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        [StringLength(50, ErrorMessage = "Username cannot exceed 50 characters.")]
        public string? Username { get; set; }

        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters.")]
        public string? Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string? ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string? Email { get; set; }

        [Phone(ErrorMessage = "Invalid phone number.")]
        [StringLength(15, ErrorMessage = "Phone number cannot exceed 15 digits.")]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        [StringLength(50, ErrorMessage = "First Name cannot exceed 50 characters.")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(50, ErrorMessage = "Last Name cannot exceed 50 characters.")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Gender is required.")]
        public GenderEnum Gender { get; set; }

        [DataType(DataType.Date)]
        public DateOnly DateOfBirth { get; set; }

        public string? AvatarUrl { get; set; }
        public IFormFile? AvatarFile { get; set; }

        public int RoleId { get; set; }
        public string? RoleName { get; set; }
        public bool IsActive { get; set; }
    }

    public enum GenderEnum
    {
        Male,
        Female,
        Unspecified
    }
}
