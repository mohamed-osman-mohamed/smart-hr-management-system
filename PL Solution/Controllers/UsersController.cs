using DAL_Solution.Models.IdentityModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PL_Solution.ViewModels.Accounts;

namespace PL_Solution.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public IActionResult Index(string? searchInput)
        {
            var Users = new List<UserViewModel>();
            if (string.IsNullOrEmpty(searchInput))
            {
                Users = _userManager.Users
                .Select(u => new UserViewModel
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    Roles = _userManager.GetRolesAsync(u).Result
                }).ToList();
            }
            else
            {
                Users = _userManager.Users
                .Where(u => u.NormalizedEmail.Contains(searchInput.ToUpper()))
                .Select(u => new UserViewModel
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    Roles = _userManager.GetRolesAsync(u).Result
                }).ToList();
            }
            return View(Users);
        }
        #region Detail Action
        public IActionResult Details(string? id, string viewName = "Details")
        {
            if (id is null) return BadRequest();
            var user = _userManager.FindByIdAsync(id).Result;
            if (user is null) return NotFound();
            var userVM = new UserViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Roles = _userManager.GetRolesAsync(user).Result
            };
            return View(viewName, userVM);
        }
        #endregion

        #region Edit Action
        public IActionResult Edit(string? id)
        {
            return Details(id, "Edit");
        }
        [HttpPost]
        [ValidateAntiForgeryToken] // to insure that the request is coming from the same site
        public IActionResult Edit([FromRoute] string? id, UserViewModel userVM)
        {
            if (id is null || id != userVM.Id) return BadRequest();
            if (ModelState.IsValid)
            {
                var user = _userManager.FindByIdAsync(userVM.Id).Result;
                if (user is null) return NotFound();
                user.FirstName = userVM.FirstName;
                user.LastName = userVM.LastName;
                user.Email = userVM.Email;
                user.UserName = userVM.Email;
                var result = _userManager.UpdateAsync(user).Result;
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(userVM);
        }
        #endregion

        public IActionResult Delete(string? id)
        {
            return Details(id, "Delete");
        }
        [HttpPost]
        [ValidateAntiForgeryToken] // to insure that the request is coming from the same site
        public IActionResult Delete(string? id, UserViewModel userVM)
        {
            if (id is null || id != userVM.Id) return BadRequest();
            if (ModelState.IsValid)
            {
                var user = _userManager.FindByIdAsync(userVM.Id).Result;
                if (user is null) return NotFound();
                var result = _userManager.DeleteAsync(user).Result;
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(userVM);
        }

    }
}
