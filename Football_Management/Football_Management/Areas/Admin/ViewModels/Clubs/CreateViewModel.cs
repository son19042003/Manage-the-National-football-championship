using System.ComponentModel.DataAnnotations;

namespace Football_Management.Areas.Admin.ViewModels.Clubs
{
    public class CreateViewModel
    {
        [Required]
        [MaxLength(10)]
        public string? ClubId { get; set; }

        [Required]
        [MaxLength(100)]
        public string? ClubName { get; set; }

        [Required]
        [MaxLength(5)]
        public string? ShortName { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Stadium { get; set; }
        public string? LogoUrl { get; set; }

        [Url]
        public string? LinkFb { get; set; }

        [Url]
        public string? LinkIg { get; set; }

        public IFormFile? LogoFile { get; set; }
        public bool IsActive { get; set; }
    }
}
