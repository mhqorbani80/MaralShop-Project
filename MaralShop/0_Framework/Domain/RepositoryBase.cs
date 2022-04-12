using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace _0_Framework.Domain
{
    public class RepositoryBase<TKet, T> : IRepository<TKet, T> where T : class
    {
        private readonly DbContext _dbContext;

        public RepositoryBase(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Create(T entity)
        {
            _dbContext.Add<T>(entity);
        }

        public bool Exists(Expression<Func<T, bool>> expression)
        {
            return _dbContext.Set<T>().Any(expression);
        }

        public List<T> GetAll()
        {
            return _dbContext.Set<T>().ToList();
        }

        public T GetBy(TKet id)
        {
            return _dbContext.Find<T>(id);
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
