namespace Football_Management.Areas.Admin.ViewModels.TypeGoals
{
    public class DeleteViewModel
    {
        public int Index { get; set; }
        public int TypeGoalId { get; set; }
        public string? TypeGoalName { get; set; }
        public string? TypeGoalDescription { get; set; }
        public bool ConfirmDelete { get; set; }
    }
}
