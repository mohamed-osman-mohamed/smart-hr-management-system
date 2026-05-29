
using DAL_Solution.Models.Employees;
using DAL_Solution.Repositories.GenericRepo;

namespace DAL_Solution.Repositories.EmployeeRepo
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        
    }
}
