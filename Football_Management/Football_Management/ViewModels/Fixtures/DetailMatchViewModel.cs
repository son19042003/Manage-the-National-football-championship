namespace Football_Management.ViewModels.Fixtures
{
    public class DetailMatchViewModel
    {
        public int MatchId { get; set; }
        public string? HomeTeamId { get; set; }
        public string? HomeTeam { get; set; }
        public string? LogoHUrl { get; set; }
        public string? AwayTeamId { get; set; }
        public string? AwayTeam { get; set; }
        public string? LogoAUrl { get; set; }
        public DateOnly DateStart { get; set; }
        public TimeOnly TimeStart { get; set; }
        public string? Stadium { get; set; }
    }
}
