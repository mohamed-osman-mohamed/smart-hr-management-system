using System.ComponentModel.DataAnnotations;

namespace BLL_Solution.DataTransferObjects.EmployeeDTOs
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int? Age { get; set; }

        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        [EmailAddress]
        public string? Email { get; set; }
        public AddressDto? Address { get; set; } = new();
        public string Gender { get; set; } = null!;

        [Display(Name = "Employee Type")]
        public string EmployeeType { get; set; } = null!;
        [Display(Name = "Department")]
        public string? DepartmentName { get; set; }

    }
}
