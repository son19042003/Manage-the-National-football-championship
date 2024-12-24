using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Football_Management.Areas.Admin.ViewModels.NewsPages
{
    public class CreateViewModel
    {
        public int NewsId { get; set; }

        [Required(ErrorMessage = "Title of news is required")]
        public string? Title { get; set; }

        //[Required]
        public DateTime DateUpdated { get; set; }

        //[Required]
        public string? ImageUrl { get; set; }
        public IFormFile? ThumbFile { get; set; }

        public string? ImgContentUrl { get; set; }
        public IFormFile? ContentFile { get; set; }

        [Required(ErrorMessage = "Content of news is required")]
        public string? Content { get; set; }

        [Required(ErrorMessage = "Type news is required")]
        public int TypeNewsId { get; set; }
        public IEnumerable<SelectListItem>? ListTypeNews { get; set; }

        public bool Status { get; set; }
    }
}
