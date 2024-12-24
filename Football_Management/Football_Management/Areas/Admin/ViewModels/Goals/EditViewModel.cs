using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Football_Management.Areas.Admin.ViewModels.Goals
{
    public class EditViewModel
    {
        public int GoalId { get; set; }
        public string? TeamGoal { get; set; }
        public int GoalIndex { get; set; }
        public string? TeamId { get; set; }

        public int PlayerId { get; set; }
        public IEnumerable<SelectListItem>? ListPlayersH { get; set; }
        //public int PlayerIdA { get; set; }
        public IEnumerable<SelectListItem>? ListPlayersA { get; set; }

        public int TypeGoalId { get; set; }
        public IEnumerable<SelectListItem>? ListTypeGoals { get; set; }

        [Required(ErrorMessage = "Time score is required.")]
        [StringLength(15, ErrorMessage = "Phone number cannot exceed 15 digits.")]
        public string? TimeScore { get; set; }

        public int Round { get; set; }
        public string? HomeTeam { get; set; }
        public int? GoalH { get; set; }
        public string? AwayTeam { get; set; }
        public int? GoalA { get; set; }
        public int PageNumber { get; set; }
        public bool IsHomeGoal { get; set; }
    }
}
