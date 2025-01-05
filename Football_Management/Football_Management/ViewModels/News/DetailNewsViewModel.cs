namespace Football_Management.ViewModels.News
{
    public class DetailNewsViewModel
    {
        public int NewsId { get; set; }
        public string? Title { get; set; }
        public string? ThumnailUrl { get; set; }
        public string? ImageContent { get; set; }
        public string? Content { get; set; }
        public DateTime DateUpdate { get; set; }
    }
}
