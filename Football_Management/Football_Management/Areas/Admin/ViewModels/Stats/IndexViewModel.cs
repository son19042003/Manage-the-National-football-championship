namespace Football_Management.Areas.Admin.ViewModels.Stats
{
    public class IndexViewModel
    {
        public int PlayerId { get; set; }
        public int Index { get; set; }
        public string? PlayerName { get; set; }
        public string? ClubName { get; set; }
        public string? LogoUrl { get; set; }
        public int? Goals { get; set; }
    }
}
