using BLL_Solution.DataTransferObjects.DepartmentDTOs;

namespace BLL_Solution.Services.Interfaces
{
    public interface IDepartmentServices
    {
        int AddDepartment(CreateDepartmentDto departmentDto);
        bool DeleteDepartment(int id);
        IEnumerable<DepartmentDto> GetAllDepartments();
        DepartmentDetailsDto? GetDepartmentById(int id);
        int UpdateDepartment(UpdateDepartmentDto departmentDto);
    }
}