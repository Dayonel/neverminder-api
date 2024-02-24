using Neverminder.Core.Entity.Base;
using System.Linq.Expressions;

namespace Neverminder.Core.Interfaces.Repositories.Base
{
    public interface IRepository<T> where T : EntityBase
    {
        Task<T> First();
        Task<T> GetById(int id);
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
        Task<T> Get(Expression<Func<T, bool>> predicate);
        Task<List<T>> List();
        Task<List<T>> List(Expression<Func<T, bool>> predicate);
        Task<List<T>> ListPaged(int page, int pageSize);
        Task<int> AddAsync(T entity);
        Task<bool> DeleteAsync(int id);
        Task<bool> UpdateAsync(T entity);
        Task<bool> UpdateRangeAsync(List<T> entities);
    }
}
