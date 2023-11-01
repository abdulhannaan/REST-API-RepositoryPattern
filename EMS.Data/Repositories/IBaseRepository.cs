using System.Linq.Expressions;

namespace EMS.Data.Repositories
{
    public interface IBaseRepository<T>
    {
        Task<T> GetByIdAsync(int id);
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter = null);
        Task<T> AddAsync(T entity);
        Task<bool> AddRangeAsync(List<T> entities);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(int id, int loginUser);
        void HardDeleteRange(List<T> entities);
        void HardDelete(T entity);
    }
}
