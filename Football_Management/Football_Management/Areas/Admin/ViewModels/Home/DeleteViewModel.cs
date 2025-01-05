namespace Football_Management.Areas.Admin.ViewModels.Home
{
    public class DeleteViewModel
    {
        public int PlayerRegisId { get; set; }
        public string? Name { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public double Height { get; set; }
        public string? Nationality { get; set; }
        public string? Position { get; set; }
        public int Number { get; set; }
        public string? AvatarPath { get; set; }
        public string? LinkFb { get; set; }
        public string? LinkIg { get; set; }
        public string? Club { get; set; }
        public bool ConfirmDelete { get; set; }
    }
}
