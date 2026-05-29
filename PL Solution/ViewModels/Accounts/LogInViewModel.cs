using System.ComponentModel.DataAnnotations;

namespace PL_Solution.ViewModels.Accounts
{
    public class LogInViewModel
    {
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
        [Display(Name ="Remember Me")]
        public bool RememberMe { get; set; }
    }
}
