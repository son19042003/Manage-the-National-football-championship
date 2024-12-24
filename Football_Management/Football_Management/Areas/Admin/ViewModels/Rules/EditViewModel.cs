using Football_Management.Models;
using Humanizer;
using System.ComponentModel.DataAnnotations;

namespace Football_Management.Areas.Admin.ViewModels.Rules
{
    public class EditViewModel
    {
        public int RuleId { get; set; }

        [Required(ErrorMessage = "Min age is required")]
        [Range(1, 40, ErrorMessage = "Min age must be at least 1 and less than 40.")]
        public int MinAge { get; set; }

        [Required(ErrorMessage = "Max foreign players is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Max foreign players must be at least 1.")]
        public int MaxForeignPlayers { get; set; }

        [Required(ErrorMessage = "Min player is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Min player must be at least 1.")]
        public int MinPlayer { get; set; }

        [Required(ErrorMessage = "Max player is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Max player must be at least 1.")]
        public int MaxPlayer { get; set; }
    }
}
