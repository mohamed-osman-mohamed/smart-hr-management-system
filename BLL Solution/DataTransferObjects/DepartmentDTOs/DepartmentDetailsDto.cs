namespace BLL_Solution.DataTransferObjects.DepartmentDTOs
{
    public class DepartmentDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;
        public string Description { get; set; } = string.Empty;
        public int CreatedBy { get; set; } 
        public DateOnly DateOfCreation { get; set; } 
        public int LastModifiedBy { get; set; } 
        public DateOnly LastModifiedOn { get; set; }
        public bool IsDeleted { get; set; } 

    }
}
