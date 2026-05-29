using System.ComponentModel.DataAnnotations;

namespace BLL_Solution.DataTransferObjects.EmployeeDTOs
{
    public class EmployeeDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int? Age { get; set; }
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        public string? Email { get; set; }
        public AddressDto? Address { get; set; } = new();
        public string? PhoneNumber { get; set; }
        [DataType(DataType.Date)] 
        public DateTime HiringDate { get; set; } 
        public string Gender { get; set; } = null!;
        public string EmployeeType { get; set; } = null!;
        public int CreatedBy { get; set; } 
        public DateTime CreatedOn { get; set; }  
        public int LastModifiedBy { get; set; } 
        public DateTime LastModifiedOn { get; set; }
        public bool IsDeleted { get; set; } 
        public int? DepartmentId { get; set; }
        [Display(Name = "Department")]
        public string? DepartmentName { get; set; }
        public string? Image { get; set; }  

    }
}
