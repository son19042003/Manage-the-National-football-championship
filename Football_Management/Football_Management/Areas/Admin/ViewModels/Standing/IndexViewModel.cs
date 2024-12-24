namespace Football_Management.Areas.Admin.ViewModels.Standing
{
    public class IndexViewModel
    {
        public int StandingId { get; set; }
        public int Index { get; set; }
        public string? ClubName { get; set; }
        public string? LogoUrl { get; set; }
        public int? Played { get; set; }
        public int? GoalDifference { get; set; }
        public int? Points { get; set; }
    }
}
