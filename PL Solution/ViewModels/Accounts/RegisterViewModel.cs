using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PL_Solution.ViewModels.Accounts
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "First Name Is Required")]
        [MaxLength(50)]
        public string FirstName { get; set; } = null!;
        [Required(ErrorMessage = "Last Name Is Required")]
        [MaxLength(50)]
        public string LastName { get; set; } = null!;
        [Required(ErrorMessage = "User Name Is Required")]
        [MaxLength(50)]
        public string UserName { get; set; } = null!;
        [EmailAddress]
        public string Email { get; set; } = null!;
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; } = null!;
        public bool IsAgree { get; set; }

    }
}
