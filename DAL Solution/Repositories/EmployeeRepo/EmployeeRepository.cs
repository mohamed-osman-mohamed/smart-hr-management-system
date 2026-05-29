using DAL_Solution.Data.Contexts;
using DAL_Solution.Models.Employees;
using DAL_Solution.Repositories.GenericRepo;

namespace DAL_Solution.Repositories.EmployeeRepo 
{
    public class EmployeeRepository : GenericRepository<Employee> , IEmployeeRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public EmployeeRepository(ApplicationDbContext dbContext) : base(dbContext) 
        {
            _dbContext = dbContext;
        }
    }
}
