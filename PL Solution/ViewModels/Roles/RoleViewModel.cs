namespace PL_Solution.ViewModels.Roles
{
    public class RoleViewModel
    {
        public string? Id { get; set; }
        public string Name { get; set; } = null!;
        public List<UserInRoleViewModel> Users { get; set; } = new List<UserInRoleViewModel>();
    }
}
    