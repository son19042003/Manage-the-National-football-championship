namespace Football_Management.Areas.Admin.ViewModels.Accounts
{
    public class DetailViewModel
    {
        public int AccountsId { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Gender { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string? AvatarUrl { get; set; }
        public string? RoleName { get; set; }
        public bool IsActive { get; set; }
    }
}
