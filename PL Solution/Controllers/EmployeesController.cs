using BLL_Solution.DataTransferObjects.EmployeeDTOs;
using BLL_Solution.Services.Interfaces;
using DAL_Solution.Models.Employees;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using P_Layer.ViewModels.Employees;

namespace P_Layer.Controllers
{
    [Authorize]
    public class EmployeesController : Controller
    {

        #region Fields & Constructor
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<EmployeesController> _logger;
        private readonly IWebHostEnvironment _env;
        public EmployeesController(IEmployeeService employeeService, ILogger<EmployeesController> logger,
                                   IWebHostEnvironment env)
        {
            _employeeService = employeeService;
            _logger = logger;
            _env = env;
        } 
        #endregion

        public IActionResult Index(string? EmployeeSearchName)
        {
            var employees = _employeeService.GetAllEmployees(EmployeeSearchName);
            return View(employees);
        }


        #region Create Employee (Add)
        [HttpGet]
        // GET: baseUrl/Employees/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        // POST: baseUrl/Employees/Create
        public IActionResult Create(EmployeeViewModel employeeVM)
        {
            // Server-side validation
            if (ModelState.IsValid)
            {
                try
                {
                    var employeeDto = new CreateEmployeeDto()
                    {
                        Name = employeeVM.Name,
                        Age = employeeVM.Age,
                        Address = employeeVM.Address,
                        Email = employeeVM.Email,
                        PhoneNumber = employeeVM.PhoneNumber,
                        Salary = employeeVM.Salary,
                        IsActive = employeeVM.IsActive,
                        Gender = employeeVM.Gender,
                        HiringDate = employeeVM.HiringDate,
                        EmployeeType = employeeVM.EmployeeType,
                        DepartmentId = employeeVM.DepartmentId,
                        Image = employeeVM.Image
                    };
                    if (_employeeService.CreateEmployee(employeeDto) > 0)
                    {
                        TempData["Message"] = "Employee Created Successfully";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        TempData["Message"] = "Failed to create employee";
                        ModelState.AddModelError(string.Empty, "Failed to create employee.");
                    }
                }
                catch (Exception ex)
                {
                    if (_env.IsDevelopment())
                        ModelState.AddModelError(string.Empty, ex.Message);
                    else
                        _logger.LogError(ex, "An error occurred while creating an employee.");
                }
            }
            return View(employeeVM);
        }
        #endregion

        #region Read Data (Details)
        [HttpGet]
        // GET: baseUrl/Employees/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null) return BadRequest(); // 400 Bad Request
            var employee = _employeeService.GetEmployeeById(id.Value);
            if (employee == null) return NotFound(); // 404 Not Found
            else return View(employee);
        }
        #endregion

        #region Update Data (Edit)
        [HttpGet]
        // GET: baseUrl/Employees/Update/{Id}
        public IActionResult Edit([FromRoute]int? id)
        {
            if (id == null) return BadRequest(); // 400 Bad Request
            var employee = _employeeService.GetEmployeeById(id.Value);
            if (employee == null) return NotFound(); // 404 Not Found
            var employeeVM = new EmployeeViewModel()
            {
                Id = employee.Id,
                Name = employee.Name,
                Age = employee.Age,
                Address = employee.Address,
                Gender = Enum.Parse<Gender>(employee.Gender),
                Salary = employee.Salary,
                IsActive = employee.IsActive,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                HiringDate = DateOnly.FromDateTime(employee.HiringDate),
                EmployeeType = Enum.Parse<EmployeeType>(employee.EmployeeType),
                DepartmentId = employee.DepartmentId,
                ImageName = employee.Image
            };
            return View(employeeVM);
        }


        [HttpPost]
        // POST: baseUrl/Employees/Update/{Id}
        public IActionResult Edit([FromRoute] int? id, EmployeeViewModel employeeVM)
        {
            if (id == null || id != employeeVM.Id) return BadRequest(); // 400 Bad Request
            if (ModelState.IsValid)
            {
                try
                {
                    var employeeDto = new UpdateEmployeeDto()
                    {
                        Id = id.Value,
                        Name = employeeVM.Name,
                        Age = employeeVM.Age,
                        Address = employeeVM.Address,
                        Gender = employeeVM.Gender,
                        Email = employeeVM.Email,
                        PhoneNumber = employeeVM.PhoneNumber,
                        Salary = employeeVM.Salary,
                        IsActive = employeeVM.IsActive,
                        HiringDate = employeeVM.HiringDate,
                        EmployeeType = employeeVM.EmployeeType,
                        DepartmentId = employeeVM.DepartmentId,
                        Image = employeeVM.Image
                    };
                    if (_employeeService.UpdateEmployee(employeeDto) > 0)
                    {
                        TempData["Message"] = "Employee Updated Successfully";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        TempData["Message"] = "Failed to Update employee";
                        ModelState.AddModelError(string.Empty, "Failed to update employee.");
                    }
                }
                catch (Exception ex)
                {
                    if (_env.IsDevelopment())
                        ModelState.AddModelError(string.Empty, ex.Message);
                    else
                        _logger.LogError(ex, "An error occurred while updating an employee.");
                }
            }
            return View(employeeVM);
        }
        #endregion

        #region Delete Data 

        [HttpGet]
        // GET: baseUrl/Employees/Delete/{Id}
        public IActionResult Delete(int? Id)
        {
            if (Id == null || Id <= 0) return BadRequest(); // 400 Bad Request
            var employee = _employeeService.GetEmployeeById(Id.Value);
            if (employee is null || employee.IsDeleted) return NotFound(); // 404 Not Found
            return View(employee);
        }

        [HttpPost]
        //POST: baseUrl/Employees/Delete/{Id}
        public IActionResult Delete([FromRoute] int id)
        {
            try
            {
                var result = _employeeService.DeleteEmployee(id);
                if (result)
                {
                    TempData["Message"] = "Employee Deleted Successfully";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["Message"] = "Failed to Delete employee";
                    _logger.LogError("An error occurred while deleting an employee.");
                }
            }
            catch (Exception ex)
            {
                if (_env.IsDevelopment())
                    ModelState.AddModelError(string.Empty, ex.Message);
                else
                    _logger.LogError(ex, "An error occurred while deleting an employee.");

            }
            return View(nameof(Index));
        }
        #endregion

        #region Search 
        [HttpGet]
        public IActionResult Search(string? EmployeeSearchName)
        {
            var employees = _employeeService.GetAllEmployees(EmployeeSearchName);
            return PartialView("_TablePartialView", employees);
        }
        #endregion

    }
}
