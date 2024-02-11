using Microsoft.EntityFrameworkCore;
using Neverminder.Core.Entity.Base;
using Neverminder.Core.Interfaces.Repositories.Base;
using System.Linq.Expressions;

namespace Neverminder.Data.Repositories.Base
{
    public abstract class BaseRepository<T> : IRepository<T> where T : EntityBase
    {
        private readonly NeverminderDbContext _dbContext;
        public BaseRepository(NeverminderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task<T> First()
        {
            return await _dbContext.Set<T>().FirstOrDefaultAsync();
        }

        public virtual async Task<T> GetById(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public virtual async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().AnyAsync(predicate);
        }

        public virtual async Task<T> Get(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().FirstOrDefaultAsync(predicate);
        }

        public virtual async Task<List<T>> List()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public virtual async Task<List<T>> List(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task<int> AddAsync(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            if (!(await _dbContext.SaveChangesAsync() > 0))
                throw new Exception($"Failed to add entity: {typeof(T)}");
            return entity.Id;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (id == default)
                return false;

            var dbEntity = await _dbContext.Set<T>().FindAsync(id);
            if (dbEntity == null)
                return false;

            _dbContext.Set<T>().Remove(dbEntity);
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
