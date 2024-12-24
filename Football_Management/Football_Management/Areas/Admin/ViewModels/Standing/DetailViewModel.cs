namespace Football_Management.Areas.Admin.ViewModels.Standing
{
    public class DetailViewModel
    {
        public int StandingId { get; set; }
        public int Index { get; set; }
        public string? ClubName { get; set; }
        public string? LogoUrl { get; set; }
        public int? Played { get; set; }
        public int? Won { get; set; }
        public int? Drawn { get; set; }
        public int? Lost { get; set; }
        public int? GoalF { get; set; }
        public int? GoalA { get; set; }
        public int? GoalDifference { get; set; }
        public int? Points { get; set; }
    }
}
