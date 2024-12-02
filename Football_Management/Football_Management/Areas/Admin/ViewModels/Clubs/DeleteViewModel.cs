namespace Football_Management.Areas.Admin.ViewModels.Clubs
{
    public class DeleteViewModel
    {
        public string? ClubId { get; set; }
        public string? ClubName { get; set; }
        public string? ShortName { get; set; }
        public string? Stadium { get; set; }
        public string? LogoUrl { get; set; }
        public string? LinkFb { get; set; }
        public string? LinkIg { get; set; }
        public bool IsActive { get; set; }
        public bool ConfirmDelete { get; set; }
    }
}
