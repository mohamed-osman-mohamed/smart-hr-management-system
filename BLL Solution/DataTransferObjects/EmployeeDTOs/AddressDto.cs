using System.ComponentModel.DataAnnotations;

namespace BLL_Solution.DataTransferObjects.EmployeeDTOs
{
    public class AddressDto
    {
        [Display(Name = "Country")]
        [Required]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Country can only contain letters and spaces.")]
        public string Country { get; set; } = null!;
        [Display(Name = "City")]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "City can only contain letters and spaces.")]
        public string? City { get; set; }
        [Display(Name = "Street")]
        [RegularExpression("^[1-9]{1,3}-[A-Za-z]{5,15}$", ErrorMessage = "Street must be like 123-Street")]
        public string? Street { get; set; }
        [Display(Name = "State")]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "State can only contain letters and spaces.")]
        public string? State { get; set; }
        [Display(Name = "Zip Code")]
        [RegularExpression(@"^\d{5}(-\d{4})?$", ErrorMessage = "ZipCode must be in the format 12345 or 12345-6789.")]
        public string? ZipCode { get; set; }
    }
}
