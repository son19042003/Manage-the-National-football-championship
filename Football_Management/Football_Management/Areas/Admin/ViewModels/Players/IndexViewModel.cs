namespace Football_Management.Areas.Admin.ViewModels.Players
{
    public class IndexViewModel
    {
        public int PlayerId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public bool IsInClub { get; set; }
        public string? ClubName { get; set; }
        public int Index { get; set; }
    }
}
