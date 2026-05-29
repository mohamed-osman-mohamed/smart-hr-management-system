using BLL_Solution.DataTransferObjects.DepartmentDTOs;
using BLL_Solution.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using P_Layer.ViewModels.Departments;

namespace P_Layer.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {

        #region Fields
        private readonly IDepartmentServices _departmentServices;
        private readonly ILogger<DepartmentDto> _logger;
        private readonly IWebHostEnvironment _env;
        #endregion

        // Constructor
        public DepartmentController(IDepartmentServices departmentServices,
                                    ILogger<DepartmentDto> logger,
                                    IWebHostEnvironment env)
        {
            _departmentServices = departmentServices;
            _logger = logger;
            _env = env;
        }

        // GET: Department Default page
        [HttpGet]
        public IActionResult Index()
        {
            var departments = _departmentServices.GetAllDepartments();
            return View(departments);
        }

        #region Create Actions
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(DepartmentViewModel departmentVM)
        {
            if (ModelState.IsValid)
            {
                var message = string.Empty;
                try
                {
                    var departmentDto = new CreateDepartmentDto()
                    {
                        Code = departmentVM.Code,
                        Name = departmentVM.Name,
                        Description = departmentVM.Description ?? string.Empty,
                        DateOfCreation = departmentVM.DateOfCreation
                    };
                    var Result = _departmentServices.AddDepartment(departmentDto);
                    if (Result > 0)
                        message = "Department Created Successfully";
                    else
                    {
                        message = "Can't Create This Department";
                        ModelState.AddModelError(string.Empty, message);
                        return View(departmentVM);
                    }
                    TempData["Message"] = message;
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    //Log Excption
                    _logger.LogError(ex, ex.Message);
                    if (_env.IsDevelopment())
                    {
                        message = ex.Message;
                        return View(departmentVM);
                    }
                    else
                    {
                        message = "Department Can't be Created";
                        return View("Error", message);
                    }
                }
            }
            else
            {
                return View(departmentVM);
            }
        }

        #endregion

        #region Read Action
        // Show Details of Department
        [HttpGet]
        // baseUrl /Department/Details/{Id}
        public IActionResult Details(int? id)
        {
            if (id == null || id <= 0)
                return BadRequest(); // status code 400

            var department = _departmentServices.GetDepartmentById(id.Value);

            if (department == null)
                return NotFound(); // status code 404

            return View(department);
        }

        #endregion

        #region Update Actions

        [HttpGet]  // Get Edit Page 
        // baseUrl /Department/Edit/{Id}
        public IActionResult Edit(int? id)
        {
            if (id == null || id <= 0)
                return BadRequest(); // status code 400

            var department = _departmentServices.GetDepartmentById(id.Value);

            if (department == null)
                return NotFound(); // status code 404

            return View(new DepartmentViewModel()
            {
                Code = department.Code,
                Name = department.Name,
                Description = department.Description,
                DateOfCreation = department.DateOfCreation
            });
        }

        [HttpPost]  // Post Edit Page [Post Edited Data]
        // baseUrl /Department/Edit/{Id}
        public IActionResult Edit([FromRoute] int id, DepartmentViewModel departmentVM)
        {
            if (!ModelState.IsValid)
                return View(departmentVM);

            var message = string.Empty;
            try
            {
                var departmentDto = new UpdateDepartmentDto()
                {
                    Id = id,
                    Code = departmentVM.Code,
                    Name = departmentVM.Name,
                    Description = departmentVM.Description ?? string.Empty,
                    DateOfCreation = departmentVM.DateOfCreation
                };
                var Result = _departmentServices.UpdateDepartment(departmentDto);
                if (Result > 0)
                {
                    TempData["Message"] = "Department Updated Successfully";
                    return RedirectToAction(nameof(Index));
                }
                message = "Can't Update This Department";
                ModelState.AddModelError(string.Empty, message);
            }
            catch (Exception ex)
            {
                //Log Excption
                if (!_env.IsDevelopment())
                {
                    TempData["Message"] = "Department Can't be Updated";
                    return View("Error", message);
                }
                TempData["Message"] = ex.Message;
                _logger.LogError(ex, ex.Message);
            }
            TempData["Message"] = message;
            return View(departmentVM);

        }

        #endregion

        #region Delete Actions
        [HttpGet] // Get Page of DeleteDepartment Details
        // baseurl / Department / Delete / {id}
        public IActionResult Delete(int? id)
        {
            if (id == null || id <= 0)
                return BadRequest(); // status code 400

            var department = _departmentServices.GetDepartmentById(id.Value);

            if (department == null)
                return NotFound(); // status code 404

            return View(department);
        }

        [HttpPost] // Post Delete Prossece
        public IActionResult Delete([FromRoute] int Id)
        {
            var message = String.Empty;
            try
            {
                var result = _departmentServices.DeleteDepartment(Id);
                if (result)
                {
                    TempData["Message"] = "Department Deleted Successfully";
                    return RedirectToAction(nameof(Index));
                }
                message = "Can't Delete This Department";
                ModelState.AddModelError(string.Empty, message);
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                message = _env.IsDevelopment() ? ex.Message : "Department Can't be Deleted";
            }
            TempData["Message"] = message;
            return View(nameof(Index));
        } 
        #endregion


    }
}
