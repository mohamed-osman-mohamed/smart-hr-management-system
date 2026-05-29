using DAL_Solution.Data.Contexts;
using DAL_Solution.Repositories.DepartmentRepo;
using DAL_Solution.Repositories.EmployeeRepo;

namespace DAL_Solution.Repositories.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private Lazy<IEmployeeRepository> _employeeRepository;
        private Lazy<IDepartmentRepository> _departmentRepository;
        private readonly ApplicationDbContext _dbContext;
        public UnitOfWork (ApplicationDbContext dbContext,
                           IEmployeeRepository employeeRepository,
                           IDepartmentRepository departmentRepository)
        {
            _dbContext = dbContext;
            _departmentRepository = new Lazy<IDepartmentRepository>(() => new DepartmentRepository(_dbContext));
            _employeeRepository = new Lazy<IEmployeeRepository> (() => new EmployeeRepository(_dbContext));
        }
        public IEmployeeRepository employeeRepository => _employeeRepository.Value;

        public IDepartmentRepository departmentRepository => _departmentRepository.Value;

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }
    }
}
