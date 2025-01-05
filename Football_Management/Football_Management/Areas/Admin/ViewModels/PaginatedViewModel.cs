using Football_Management.Models;
using Football_Management.ViewModels.Clubs;

namespace Football_Management.Areas.Admin.ViewModels
{
    public class PaginatedViewModel<T>
    {
        public List<T> Items { get; set; } = new List<T>();
        public int TotalItems { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);

        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;

        public List<ClubViewModel>? Clubs { get; set; }
        public string? ClubId { get; set; }
    }

    public class ClubViewModel
    {
        public string? ClubId { get; set; }
        public string? ClubName { get; set; }
        public bool IsActive { get; set; }
    }
}
