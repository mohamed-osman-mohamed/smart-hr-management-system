using System.ComponentModel.DataAnnotations;

namespace BLL_Solution.DataTransferObjects.DepartmentDTOs
{
    public class CreateDepartmentDto
    {
        public string Name { get; set; } = null!;
        [Range(10, int.MaxValue)]
        public string Code { get; set; } = null!;
        public string Description { get; set; } = string.Empty;
        public DateOnly DateOfCreation { get; set; }
    }
}
