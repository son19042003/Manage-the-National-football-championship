namespace Football_Management.ViewModels.Home
{
    public class MatchViewModel
    {
        public string? HomeTeam { get; set; }
        public string? LogoHome { get; set; }
        public string? AwayTeam { get; set; }
        public string? LogoAway { get; set; }
        public TimeOnly TimeStart { get; set; }
        public DateOnly DateStart { get; set; }
        public int? GoalsH { get; set; }
        public int? GoalsA { get; set; }
        public string? Status { get; set; }
        public int Round { get; set; }
    }
}
