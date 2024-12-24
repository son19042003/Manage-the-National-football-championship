namespace Football_Management.Areas.Admin.ViewModels.Roles
{
    public class DeleteViewModel
    {
        public int RoleId { get; set; }
        public string? RoleName { get; set; }
        public string? RoleDescription { get; set; }
        public bool ConfirmDelete { get; set; }
    }
}
