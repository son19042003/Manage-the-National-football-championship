namespace Football_Management.ViewModels.Home
{
    public class HomeViewModel
    {
        public IEnumerable<MatchViewModel>? NextRoundMatches { get; set;}
        public IEnumerable<NewsViewModel>? LatestNews { get; set; }
    }
}
