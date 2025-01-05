namespace Football_Management.ViewModels.Clubs
{
    public class DetailPlayerViewModel
    {
        public int PlayerId { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public double Height { get; set; }
        public string? Nationality { get; set; }
        public string? Position { get; set; }
        public int ShirtNumber { get; set; }
        public int? Goals { get; set; }
        public string? Avatar { get; set; }
        public string? LinkFb { get; set; }
        public string? LinkIg { get; set; }
        public string? ClubId { get; set; }
        public string? ClubName { get; set; }
        public string? PlayerName { get; set; }
        public int Age { get; set; }
    }
}
