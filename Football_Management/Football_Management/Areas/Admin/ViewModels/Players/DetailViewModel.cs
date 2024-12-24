namespace Football_Management.Areas.Admin.ViewModels.Players
{
    public class DetailViewModel
    {
        public int PlayerId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? AvatarUrl { get; set; }
        public DateOnly Birthday { get; set; }
        public double Height { get; set; }
        public string? Nationality { get; set; }
        public bool DomesticPlayer { get; set; }
        public string? Position { get; set; }
        public int Number { get; set; }
        public string? ClubName { get; set; }
        public string? LinkFb { get; set; }
        public string? LinkIg { get; set; }
        public bool IsInClub { get; set; }
        public int Goals { get; set; }
    }
}
