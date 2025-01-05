using Microsoft.Identity.Client;

namespace Football_Management.ViewModels.News
{
    public class NewsViewModel
    {
        public int NewsId { get; set; }
        public string? Title { get; set; }
        public string? ThumnailUrl { get; set; }
        public DateTime DateUpdate { get; set; }
    }
}
