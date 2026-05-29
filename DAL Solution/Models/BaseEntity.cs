namespace DAL_Solution.Models
{
    public class BaseEntity
    {
        public int Id { get; set; } // Primary Key
        public int CreatedBy { get; set; } // User ID who created the record
        public DateTime CreatedOn { get; set; } // Timestamp of creation
        public int LastModifiedBy { get; set; } // User ID who last modified the record
        public DateTime LastModifiedOn { get; set; } // Timestamp of last modification
        public bool IsDeleted { get; set; } // Soft delete flag
    }
}
