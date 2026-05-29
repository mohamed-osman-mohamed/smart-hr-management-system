using DAL_Solution.Repositories.DepartmentRepo;
using DAL_Solution.Repositories.EmployeeRepo;

namespace DAL_Solution.Repositories.UnitOfWork
{
    public interface IUnitOfWork 
    {
        public IEmployeeRepository employeeRepository { get; }
        public IDepartmentRepository departmentRepository { get; }
        public int SaveChanges();
    }
}
