namespace Football_Management.Areas.Admin.ViewModels.Goals
{
    public class DetailViewModel
    {
        public int GoalId { get; set; }
        public string? TeamGoal { get; set; }
        public int GoalIndex { get; set; }
        public string? HomeTeam { get; set; }
        public string? AwayTeam { get; set; }
        public int? GoalH { get; set; }
        public int? GoalA { set; get; }
        public int Round { get; set; }
        public string? PlayerScore { get; set; }
        public string? TimeScore { get; set; }
        public string? GoalType { get; set; }
    }
}
