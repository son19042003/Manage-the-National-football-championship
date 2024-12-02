namespace Football_Management.Areas.Admin.ViewModels.Clubs
{
    public class IndexViewModel
    {
        public int Index { get; set; }
        public string? ClubId { get; set; }
        public string? ClubName { get; set; }
        public string? LogoUrl { get; set; }
        public string? Stadium {  get; set; }
        public bool IsActive { get; set; }
    }
}
