using System.ComponentModel.DataAnnotations;

namespace Football_Management.Areas.Admin.ViewModels.Standing
{
    public class EditViewModel
    {
        public int StandingId { get; set; }
        public int Index { get; set; }
        public string? ClubName { get; set; }
        public string? LogoUrl { get; set; }
        public int? Played { get; set; }
        public int? Won { get; set; }
        public int? Drawn { get; set; }
        public int? Lost { get; set; }
        public int? GoalF { get; set; }
        public int? GoalA { get; set; }
        public int? GoalDifference { get; set; }
        
        [Required(ErrorMessage = "Points is required!")]
        [Range(0, 114, ErrorMessage = "Points cannot be less than 0 and greater than 114")]
        public int? Points { get; set; }
    }
}
