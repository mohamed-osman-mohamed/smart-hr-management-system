using BLL_Solution.DataTransferObjects.DepartmentDTOs;
using BLL_Solution.Factories;
using BLL_Solution.Services.Interfaces;
using DAL_Solution.Models;
using DAL_Solution.Repositories.DepartmentRepo;
using DAL_Solution.Repositories.UnitOfWork;

namespace BLL_Solution.Services.Classes
{
    public class DepartmentServices : IDepartmentServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentServices(IUnitOfWork unitOfWork) // constructor injection
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<DepartmentDto> GetAllDepartments()
        {
            return _unitOfWork.departmentRepository.GetAll().Select(D => D.ToDepartmentDto());
        } //Get All Departments
        public DepartmentDetailsDto? GetDepartmentById(int id)
        {
            var department = _unitOfWork.departmentRepository.GetById(id);

            return department == null ? null : department.ToDepartmentDetailsDto();
        } // Get Department By Id
        public int AddDepartment(CreateDepartmentDto departmentDto)
        {
            _unitOfWork.departmentRepository.Add(departmentDto.ToEntity());
            return _unitOfWork.SaveChanges();
        } // Add New Department
        public int UpdateDepartment(UpdateDepartmentDto departmentDto)
        {
            _unitOfWork.departmentRepository.Update(departmentDto.ToEntity());
            return _unitOfWork.SaveChanges();
        } // Update Exist Department
        public bool DeleteDepartment(int id)
        {
            var department = _unitOfWork.departmentRepository.GetById(id);
            if (department is null)
            {
                return false;
            }
            else
            {
                _unitOfWork.departmentRepository.Remove(department);
                var result = _unitOfWork.SaveChanges();
                return result > 0 ? true : false;
            }
        } // Delete Department
    }
}
