namespace Football_Management.Areas.Admin.ViewModels.Goals
{
    public class IndexViewModel
    {
        public int GoalId { get; set; }
        public int Index { get; set; }
        public string? PlayerName { get; set; }
        public string? ClubName { get; set; }
        public string? TypeGoal { get; set; }
        public int Round { get; set; }
    }
}
