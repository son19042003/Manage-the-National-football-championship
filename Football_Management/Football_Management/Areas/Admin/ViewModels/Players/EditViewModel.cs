using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Football_Management.Areas.Admin.ViewModels.Players
{
    public class EditViewModel
    {
        public int PlayerId { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        [MaxLength(50)]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [MaxLength(50)]
        public string? LastName { get; set; }

        public string? AvatarUrl { get; set; }

        public IFormFile? AvatarFile { get; set; }

        [Required(ErrorMessage = "Date of birth is required.")]
        [DataType(DataType.Date)]
        public DateOnly Birthday { get; set; }

        [Required(ErrorMessage = "Height is required.")]
        public double Height { get; set; }

        [Required(ErrorMessage = "Nationality is required.")]
        [MaxLength(50)]
        public string? Nationality { get; set; }

        public bool DomesticPlayer { get; set; }

        [Required(ErrorMessage = "Position is required.")]
        public string? Position { get; set; }

        public IEnumerable<SelectListItem>? Positions { get; set; }

        [Range(1, 99, ErrorMessage = "Shirt number must be between 1 and 99.")]
        [Required(ErrorMessage = "Shirt number is required.")]
        public int Number { get; set; }

        public string? ClubId { get; set; }

        public IEnumerable<SelectListItem>? Clubs { get; set; }

        [Url]
        public string? LinkFb { get; set; }

        [Url]
        public string? LinkIg { get; set; }

        public bool IsInClub { get; set; }
        public int PageNumber { get; set; }
    }
}
