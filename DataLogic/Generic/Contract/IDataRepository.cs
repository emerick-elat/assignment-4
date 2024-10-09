using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataLogic.Generic.Contract
{
    public interface IDataRepository<T> where T : class
    {
        Task<ICollection<T>> GetAllAsync();
        Task<T> GetEntityByIdAsync(object id);
        Task<bool> EntityExists(Expression<Func<T, bool>> predicate);
        Task<T> CreateEntityAsync(T entity);
        Task UpdateEntityAsync(object key, T entity);
        Task DeleteEntityAsync(object key);
    }
}
