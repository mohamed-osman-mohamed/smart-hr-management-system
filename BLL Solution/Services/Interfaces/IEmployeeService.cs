using BLL_Solution.DataTransferObjects.EmployeeDTOs;

namespace BLL_Solution.Services.Interfaces
{
    public interface IEmployeeService
    {
        IEnumerable<EmployeeDto> GetAllEmployees(string? EmployeeSearchName, bool WithTracking = false);
        EmployeeDetailsDto? GetEmployeeById(int id);
        int CreateEmployee(CreateEmployeeDto employeeDto);
        int UpdateEmployee(UpdateEmployeeDto employeeDto);
        bool DeleteEmployee(int id);
    }
}
