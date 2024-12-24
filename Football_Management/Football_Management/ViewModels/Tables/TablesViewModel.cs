namespace Football_Management.ViewModels.Tables
{
    public class TablesViewModel
    {
        public int Index { get; set; }
        public string? ClubName { get; set; }
        public string? Logo { get; set; }
        public int? Played { get; set; }
        public int? Won { get; set; }
        public int? Drawn { get; set; }
        public int? Lost { get; set; }
        public int? GoalsFor { get; set; }
        public int? GoalsAgainst { get; set; }
        public int? GoalsDifference { get; set; }
        public int? Points { get; set; }
    }
}
