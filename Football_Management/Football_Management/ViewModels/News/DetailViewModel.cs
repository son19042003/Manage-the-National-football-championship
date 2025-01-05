using Football_Management.ViewModels.News;

namespace Football_Management.ViewModels.News
{
    public class DetailViewModel
    {
        public DetailNewsViewModel? DetailNews { get; set; }
        public IEnumerable<LatestNewsViewModel>? LatestNews { get; set; }
    }
}
