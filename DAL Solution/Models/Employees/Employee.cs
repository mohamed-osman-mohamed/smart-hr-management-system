using DAL_Solution.Models.Departments;
namespace DAL_Solution.Models.Employees
{
    public class Employee : BaseEntity
    {
        public string Name { get; set; } = null!;
        public int? Age { get; set; }
        public Address Address { get; set; } = null!;
        public decimal Salary { get; set; }
        public string Email { get; set; }= null!;
        public DateTime HiringDate { get; set; }
        public string PhoneNumber { get; set; } = null!;
        public bool IsActive { get; set; }
        public Gender Gender { get; set; }
        public EmployeeType EmployeeType { get; set; }
        public int? DepartmentId { get; set; }  
        public virtual Department? Department { get; set; }
        public string? ImageName { get; set; }
    }
}
