using System.ComponentModel.DataAnnotations;

namespace P_Layer.ViewModels.Departments
{
    public class DepartmentViewModel
    {
        public string Name { get; set; } = null!;
        [Range(10, int.MaxValue)]
        public string Code { get; set; } = null!;
        public string? Description { get; set; }
        public DateOnly DateOfCreation { get; set; }

    }
}
