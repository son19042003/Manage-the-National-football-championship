using System.ComponentModel.DataAnnotations;

namespace Football_Management.Areas.Admin.ViewModels.TypeGoals
{
    public class CreateViewModel
    {
        public int TypeGoalId { get; set; }

        [Required(ErrorMessage = "Type goal name is required")]
        public string? TypeGoalName { get; set; }

        [Required(ErrorMessage = "Type goal description is required")]
        public string? TypeGoalDescription { get; set; }
    }
}
