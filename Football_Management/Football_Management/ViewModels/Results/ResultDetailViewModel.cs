namespace Football_Management.ViewModels.Results
{
    public class ResultDetailViewModel
    {
        public ScoreViewModel? Score { get; set; }
        public IEnumerable<PlayerScoreViewModel>? PlayerScore { get; set; }
    }
}
