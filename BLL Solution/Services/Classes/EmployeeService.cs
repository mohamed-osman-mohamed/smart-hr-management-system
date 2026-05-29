using AutoMapper;
using BLL_Solution.DataTransferObjects.EmployeeDTOs;
using BLL_Solution.Services.Interfaces;
using DAL_Solution.Models.Employees;
using DAL_Solution.Repositories.EmployeeRepo;
using DAL_Solution.Repositories.UnitOfWork;

namespace BLL_Solution.Services.Classes
{
    public class EmployeeService : IEmployeeService
    {
        #region Fields & Constructor
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAttachmentService _attachmentService;
        private readonly IMapper _mapper;

        public EmployeeService(IUnitOfWork unitOfWork,
                               IAttachmentService attachmentService,
                               IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _attachmentService = attachmentService;
            _mapper = mapper;
        }
        #endregion

        // service methods

        #region Get All Employees
        public IEnumerable<EmployeeDto> GetAllEmployees(string? EmployeeSearchName, bool WithTracking = false)
        {
            IEnumerable<Employee> employees;
            if (string.IsNullOrWhiteSpace(EmployeeSearchName) || WithTracking)
            {
                employees = _unitOfWork.employeeRepository.GetAll(WithTracking);
            }
            else
            {
                employees = _unitOfWork.employeeRepository.GetAll(e => e.Name.ToLower().Contains(EmployeeSearchName.ToLower()) && !e.IsDeleted);
            }
            return _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeDto>>(employees);
        }
        #endregion

        #region Get Employee by Id
        public EmployeeDetailsDto? GetEmployeeById(int id)
        {
            var employee = _unitOfWork.employeeRepository.GetById(id);
            return employee == null ? null : _mapper.Map<Employee,EmployeeDetailsDto>(employee);
        }
        #endregion

        #region Create New Employee
        public int CreateEmployee(CreateEmployeeDto employeeDto)
        {
            var employee = _mapper.Map<CreateEmployeeDto, Employee>(employeeDto);
            if (employeeDto.Image is not null)
                employee.ImageName = _attachmentService.Upload(employeeDto.Image, "Images");
            _unitOfWork.employeeRepository.Add(employee);
            return _unitOfWork.SaveChanges();
        }
        #endregion

        #region Update Employee
        public int UpdateEmployee(UpdateEmployeeDto employeeDto)
        {
            var employee = _mapper.Map<UpdateEmployeeDto, Employee>(employeeDto);
            // Upload the image and set the ImageName property on the Employee entity
            if (employeeDto.Image is not null && employeeDto.Image.FileName != employee.ImageName)
            {
                if (!string.IsNullOrEmpty(employee.ImageName))
                {
                    _attachmentService.Delete(employee.ImageName);
                }
                employee.ImageName = _attachmentService.Upload(employeeDto.Image, "Images");
            }
                 
            _unitOfWork.employeeRepository.Update(employee);
            return _unitOfWork.SaveChanges();
        }
        #endregion

        #region Delete Employee
        public bool DeleteEmployee(int id)
        {
            var employee = _unitOfWork.employeeRepository.GetById(id);
            if (employee is null || employee.IsDeleted) return false;
            employee.IsDeleted = true;
            _unitOfWork.employeeRepository.Update(employee);
            var result = _unitOfWork.SaveChanges();
            if (result > 0) return true;
            return false;
        }
        #endregion

    }
}

