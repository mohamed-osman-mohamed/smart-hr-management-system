using DAL_Solution.Data.Contexts;
using DAL_Solution.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DAL_Solution.Repositories.GenericRepo
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly ApplicationDbContext _dbContext;

        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // CRUD Operations for TEntity will be implemented here

        #region GetAll() 
        // Get All
        public IEnumerable<TEntity> GetAll(bool withTracking = false)
        {
            if (withTracking)
            {
                return _dbContext.Set<TEntity>().Where(T => T.IsDeleted != true)
                                                .ToList();
            }
            else
            {
                return _dbContext.Set<TEntity>().AsNoTracking()
                                                .Where(T => T.IsDeleted != true)
                                                .ToList();
            }
        }
        public IEnumerable<TResult> GetAll<TResult>(Expression<Func<TEntity, TResult>> selector)
        {
            return _dbContext.Set<TEntity>()
                .Where(e => e.IsDeleted != true)
                .Select(selector)
                .ToList();
        }
        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate)
        {
                return _dbContext.Set<TEntity>().Where(predicate).ToList();
        }
        #endregion

        #region GetById()
        // Get By Id
        public TEntity? GetById(int id)
        {
            return _dbContext.Set<TEntity>().Find(id);
        }
        #endregion

        #region Add()
        // Insert 
        public void Add(TEntity entity)
        {
            _dbContext.Add(entity);
        }
        #endregion

        #region Update()
        // Update 
        public void Update(TEntity entity)
        {
            _dbContext.Update(entity);
        }
        #endregion

        #region Remove()
        // Remove  
        public void Remove(TEntity entity)
        {
            _dbContext.Remove(entity);
        }

        #endregion
    }
}
