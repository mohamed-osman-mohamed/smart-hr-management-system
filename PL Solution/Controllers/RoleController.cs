using DAL_Solution.Models.IdentityModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PL_Solution.ViewModels.Roles;

namespace PL_Solution.Controllers
{
    [Authorize (Roles = "Admin") ]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public RoleController(RoleManager<IdentityRole> roleManager,
                               UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public IActionResult Index(string? searchInput)
        {
            var roles = new List<RoleViewModel>();
            if (string.IsNullOrEmpty(searchInput))
            {
                roles = _roleManager.Roles.Select(r => new RoleViewModel
                {
                    Id = r.Id,
                    Name = r.Name ?? "Role Name Not Found"
                })
                    .ToList();

            }
            else
            {
                roles = _roleManager.Roles
                    .Where(r => r.NormalizedName.Contains(searchInput.ToUpper()))
                    .Select(r => new RoleViewModel
                    {
                        Id = r.Id,
                        Name = r.Name ?? "Role Name Not Found"
                    })
                    .ToList();
            }
            return View(roles);
        }

        #region Create Action
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(RoleViewModel roleViewModel)
        {
            if (ModelState.IsValid)
            {
                var role = new IdentityRole
                {
                    Name = roleViewModel.Name
                };
                var result = _roleManager.CreateAsync(role).Result;
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(roleViewModel);
        }
        #endregion

        #region Details
        [HttpGet]
        public IActionResult Details(string? id, string viewName = "Details")
        {
            if (id is null) return BadRequest();
            var role = _roleManager.FindByIdAsync(id).Result;
            if (role is null) return NotFound();
            var roleVM = new RoleViewModel
            {
                Id = role.Id,
                Name = role.Name ?? "Role Name Not Found"
            };
            return View(viewName, roleVM);
        }
        #endregion

        #region Edit Action
        public IActionResult Edit(string? id)
        {
            if (id is null) return BadRequest();
            var role = _roleManager.FindByIdAsync(id).Result;
            if (role == null) return NotFound();
            var model = new RoleViewModel { Id = role.Id, Name = role.Name };

            foreach (var user in _userManager.Users.ToList())
            {
                model.Users.Add(new UserInRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    IsSelected = _userManager.IsInRoleAsync(user, role.Name).Result
                });
            }
            return View(model);
            //return Details(id, "Edit");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] string? id, RoleViewModel roleViewModel)
        {
            if (id is null || id != roleViewModel.Id) return BadRequest();
            if (ModelState.IsValid)
            {
                var role = _roleManager.FindByIdAsync(roleViewModel.Id).Result;
                if (role is null) return NotFound();
                role.Name = roleViewModel.Name;
                var result = _roleManager.UpdateAsync(role).Result;
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(roleViewModel);
        }
        #endregion

        #region Delete Action
        public IActionResult Delete(string id)
        {
            return Details(id, "Delete");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] string? id, RoleViewModel roleViewModel)
        {
            if (id is null || id != roleViewModel.Id) return BadRequest();
            var role = _roleManager.FindByIdAsync(id).Result;
            if (role is null) return NotFound();
            var result = _roleManager.DeleteAsync(role).Result;
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(roleViewModel);
        }
        #endregion

        #region AddOrRemovesUsers
        [HttpGet]
        public IActionResult AddOrRemoveUsers(string? roleId)
        {
            if (roleId is null) return BadRequest();
            var role = _roleManager.FindByIdAsync(roleId).Result;
            if (role is null) return NotFound();

            ViewData["RoleId"] = role.Id;

            var usersInRole = new List<UserInRoleViewModel>();
            var users = _userManager.Users.ToList();
            foreach (var user in users)
            {
                var userInRole = new UserInRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName ?? "User Name Not Found",
                    IsSelected = _userManager.IsInRoleAsync(user, role.Name ?? string.Empty).Result
                    // returns true if user is in role
                };
                usersInRole.Add(userInRole);
            }
            return View(usersInRole);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddOrRemoveUsers(string? roleId, List<UserInRoleViewModel> usersInRoleVM)
        {
            if (roleId is null) return BadRequest();

            var role = _roleManager.FindByIdAsync(roleId).Result;
            if (role is null) return NotFound();

            if (ModelState.IsValid)
            {
                var flag = true;
                IdentityResult result = null!;
                foreach (var user in usersInRoleVM)
                {
                    var appUser = _userManager.FindByIdAsync(user.UserId).Result;
                    if (appUser is not null)
                    {
                        if (user.IsSelected && !_userManager.IsInRoleAsync(appUser, role.Name ?? string.Empty).Result)
                        {
                            result = _userManager.AddToRoleAsync(appUser, role.Name ?? string.Empty).Result;
                        }
                        else if (!user.IsSelected && _userManager.IsInRoleAsync(appUser, role.Name ?? string.Empty).Result)
                        {
                            result = _userManager.RemoveFromRoleAsync(appUser, role.Name ?? string.Empty).Result;
                        }
                        if (result is not null && !result.Succeeded)
                        {
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError(string.Empty, error.Description);
                            }
                            flag = false;
                            result = null;
                        }
                    }
                }
                if (flag)
                {
                    return RedirectToAction(nameof(Edit), new {id = roleId});
                }

            }
            return View(usersInRoleVM);
        }


        #endregion

        
    }
}
