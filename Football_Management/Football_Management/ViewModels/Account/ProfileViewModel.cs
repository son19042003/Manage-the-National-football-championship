namespace Football_Management.ViewModels.Account
{
    public class ProfileViewModel
    {
        public int AccountId { get; set; }
        public string? Username { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public DateOnly DayOfBirth { get; set; }
        public string? Gender { get; set; }
        public string? PhoneNumber { get; set; }
        public string? AvatarUrl { get; set; }
        public string? RoleName { get; set; }
    }
}
