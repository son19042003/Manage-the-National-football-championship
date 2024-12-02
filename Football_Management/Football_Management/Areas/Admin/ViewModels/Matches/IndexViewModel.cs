namespace Football_Management.Areas.Admin.ViewModels.Matches
{
    public class IndexViewModel
    {
        public int MatchId { get; set; }
        public int Round { get; set; }
        public string? HomeTeam { get; set; }
        public string? AwayTeam { get; set; }
        public string? Status { get; set; }
        public TimeOnly TimeStart { get; set; }
        public DateOnly DateStart { get; set; }
        public int Index { get; set; }
    }
}
