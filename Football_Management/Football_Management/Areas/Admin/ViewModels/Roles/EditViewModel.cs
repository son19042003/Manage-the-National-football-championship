using System.ComponentModel.DataAnnotations;

namespace Football_Management.Areas.Admin.ViewModels.Roles
{
    public class EditViewModel
    {
        public int RoleId { get; set; }

        [Required(ErrorMessage = "Role's name is required")]
        [MaxLength(50)]
        public string? RoleName { get; set; }

        [Required(ErrorMessage = "Role's description is required")]
        [MaxLength(250)]
        public string? RoleDescription { get; set; }
    }
}
