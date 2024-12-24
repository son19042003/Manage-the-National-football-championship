using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Football_Management.Areas.Admin.ViewModels.Matches
{
    public class EditViewModel
    {
        public int MatchId { get; set; }
        public int Round { get; set; }
        public string? HomeTeam { get; set; }
        public string? AwayTeam { get; set; }

        [Required]
        public string? Status { get; set; }

        public List<SelectListItem>? StatusOptions { get; set; }

        [Required]
        public TimeOnly TimeStart { get; set; }

        [Required]
        public DateOnly DateStart { get; set; }

        [Required]
        public int GoalH { get; set; }

        [Required]
        public int GoalA { get; set; }
        public string? Stadium { get; set; }
        public int PageNumber { get; set; }
    }
}
