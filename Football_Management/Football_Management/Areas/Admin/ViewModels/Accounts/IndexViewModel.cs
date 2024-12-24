namespace Football_Management.Areas.Admin.ViewModels.Accounts
{
    public class IndexViewModel
    {
        public int Index { get; set; }
        public int AccountId { get; set; }
        public string? UserName { get; set; }
        public string? RoleName { get; set; }
        public bool IsActive { get; set; }
    }
}
