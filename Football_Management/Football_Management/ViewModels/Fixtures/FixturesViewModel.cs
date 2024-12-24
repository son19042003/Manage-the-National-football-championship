namespace Football_Management.ViewModels.Fixtures
{
    public class FixturesViewModel
    {
        public int MatchId { get; set; }
        public string? HomeTeam { get; set; }
        public string? LogoHome { get; set; }
        public string? AwayTeam { get; set; }
        public string? LogoAway { get; set; }
        public TimeOnly TimeStart { get; set; }
        public DateOnly DateStart { get; set; }
        public string? Stadium { get; set; }
    }
}
