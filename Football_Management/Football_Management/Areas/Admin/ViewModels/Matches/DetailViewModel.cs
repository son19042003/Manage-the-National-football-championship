namespace Football_Management.Areas.Admin.ViewModels.Matches
{
    public class DetailViewModel
    {
        public int MatchId { get; set; }
        public int Round { get; set; }
        public string? HomeTeam { get; set; }
        public string? AwayTeam { get; set; }
        public string? Status { get; set; }
        public TimeOnly TimeStart { get; set; }
        public DateOnly DateStart { get; set; }
        public int GoalH { get; set; }
        public int GoalA { get; set; }
        public string? Stadium { get; set; }
        public string? LogoHomeTeam { get; set; }
        public string? LogoAwayTeam { get; set; }
    }
}
