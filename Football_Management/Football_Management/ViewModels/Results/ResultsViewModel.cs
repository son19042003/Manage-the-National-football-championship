namespace Football_Management.ViewModels.Results
{
    public class ResultsViewModel
    {
        public int MatchId { get; set; }
        public string? HomeTeam { get; set; }
        public string? LogoHome { get; set; }
        public int? GoalsH { get; set; }
        public string? AwayTeam { get; set; }
        public string? LogoAway { get; set; }
        public int? GoalsA { get; set; }
        public DateOnly DateStart { get; set; }
        public string? Stadium { get; set; }
    }
}
