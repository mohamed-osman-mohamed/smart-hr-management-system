using DAL_Solution.Data.Contexts;
using DAL_Solution.Models.Departments;
using DAL_Solution.Repositories.GenericRepo;
using Microsoft.EntityFrameworkCore;

namespace DAL_Solution.Repositories.DepartmentRepo
{
    public class DepartmentRepository : GenericRepository<Department> , IDepartmentRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public DepartmentRepository(ApplicationDbContext dbContext) : base(dbContext) 
        {
            _dbContext = dbContext;
        }
        
    }
}
