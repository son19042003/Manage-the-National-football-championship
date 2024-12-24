namespace Football_Management.Areas.Admin.ViewModels.NewsPages
{
    public class DetailViewModel
    {
        public int NewsId { get; set; }
        public string? Title { get; set; }
        public DateTime DateUpdated { get; set; }
        public string? ImageUrl { get; set; }
        public string? ImgContentUrl { get; set; }
        public string? Content { get; set; }
        public string? TypeNewsName { get; set; }
        public bool? Status { get; set; }
    }
}
