using System.ComponentModel.DataAnnotations;

namespace Football_Management.Areas.Admin.ViewModels.NewsTypes
{
    public class CreateViewModel
    {
        public int TypeNewsId { get; set; }

        [Required(ErrorMessage = "Type news name is required")]
        public string? TypeNewsName { get; set; }

        [Required(ErrorMessage = "Type news description is required")]
        public string? TypeNewsDescription { get; set; }
    }
}
