namespace Football_Management.Areas.Admin.ViewModels.NewsTypes
{
    public class DeleteViewModel
    {
        public int Index { get; set; }
        public int TypeNewsId { get; set; }
        public string? TypeNewsName { get; set; }
        public string? TypeNewsDescription { get; set; }
        public bool ConfirmDelete { get; set; }
    }
}
