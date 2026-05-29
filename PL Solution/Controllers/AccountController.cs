using DAL_Solution.Models.IdentityModels;
using MailKit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using P_Layer.Controllers;
using PL_Solution.Utilties;
using PL_Solution.ViewModels.Accounts;

namespace PL_Solution.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailService _emailService;

        public AccountController(UserManager<ApplicationUser> userManager,
                                  SignInManager<ApplicationUser> signInManager,
                                  IEmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
        }

        // Register Action
        #region Register Action
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(RegisterViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var User = _userManager.FindByNameAsync(viewModel.UserName).Result;
                if (User == null)
                {
                    User = new ApplicationUser()
                    {
                        FirstName = viewModel.FirstName,
                        LastName = viewModel.LastName,
                        Email = viewModel.Email,
                        UserName = viewModel.UserName
                    };
                    var result = _userManager.CreateAsync(User, viewModel.Password).Result;
                    if (result.Succeeded)
                    {
                        return RedirectToAction("LogIn");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "This User Name Is Alredy Exist, Please Try Another One ");
                }
            }
            return View(viewModel);
        }
        #endregion

        // LogIn
        #region LogIn Actions
        public IActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public IActionResult LogIn(LogInViewModel logInViewModel)
        {
            if (ModelState.IsValid && logInViewModel is not null)
            {
                var user = _userManager.FindByEmailAsync(logInViewModel.Email).Result;
                if (user is not null)
                {
                    var flag = _userManager.CheckPasswordAsync(user, logInViewModel.Password).Result;
                    if (flag)
                    {
                        // check Account
                        var result = _signInManager.PasswordSignInAsync(user, logInViewModel.Password, logInViewModel.RememberMe, false).Result;
                        if (result.IsNotAllowed) { ModelState.AddModelError(string.Empty, "Your Account Is Not Allowed"); }
                        if (result.IsLockedOut) { ModelState.AddModelError(string.Empty, "Your Account Is Locked Out"); }
                        if (result.Succeeded) { return RedirectToAction(nameof(HomeController.Index), "Home"); }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Incorrect Email or Password");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, " Invalid LogIn");
                }
            }
            return View(logInViewModel);
        }
        #endregion

        // LogOut
        #region Log Out Action
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken] // for more security [Cross Site Request Forgery Attack]
        public IActionResult LogOut()
        {
            _signInManager.SignOutAsync().GetAwaiter().GetResult();
            return RedirectToAction(nameof(LogIn));
        }
        #endregion


        #region Forget Password
        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SendResetPasswordLink(ForgetPasswordViewModel forgetPasswordView)
        {
            if (ModelState.IsValid)
            {
                var user = _userManager.FindByEmailAsync(forgetPasswordView.Email).Result;
                if (user is not null)
                {
                    var Token = _userManager.GeneratePasswordResetTokenAsync(user).Result;
                    var ResetPasswordLink = Url.Action("ResetPassword", "Account",
                        new { forgetPasswordView.Email, Token },
                        Request.Scheme);
                    var email = new Email()
                    {
                        To = forgetPasswordView.Email,
                        Subject = "Reset Password ",
                        Body = ResetPasswordLink ?? "No Action"
                    };
                    _emailService.SendEmail(email);
                    //EmailSettings.SendEmail(email);
                    return RedirectToAction(nameof(CheckYourInbox));
                }
            }
            ModelState.AddModelError(string.Empty, "Invalid Email");
            return View(nameof(ForgetPassword), forgetPasswordView);
        }
        #endregion

        [HttpGet]
        public IActionResult CheckYourInbox()
        {
            return View();
        }

        #region RestPassword Action
        // ResetPasswork
        //P@$$W0rd
        [HttpGet]
        public IActionResult ResetPassword(string email, string token)
        {
            TempData["email"] = email;
            TempData["token"] = token;
            return View();
        }
        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordViewModel resetPasswordView)
        {
            if (!ModelState.IsValid) return View(resetPasswordView);

            string email = TempData["email"] as string ?? string.Empty;
            string token = TempData["token"] as string ?? string.Empty;

            var user = _userManager.FindByEmailAsync(email).Result;
            if (user is not null)
            {
                var result = _userManager.ResetPasswordAsync(user, token, resetPasswordView.Password).Result;
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(LogIn));
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(resetPasswordView);

        }
        #endregion
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
