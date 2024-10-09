using DataLogic.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataLogic.Generic.Contract
{
    public interface IDataRepositoryBase<T> where T : class
    {
        Task<ICollection<T>> GetEntitiesAsync();
        Task<ICollection<T>> GetQueryAsync(Expression<Func<T, bool>> expression);
        Task<T> GetEntityAsync(object id);
        Task<bool> EntityExists(Expression<Func<T, bool>> predicate);
        Task AddEntityAsync(T entity);
        void UpdateEntity(T entity);
        void DeleteEntity(T entity);
        Task SaveChangesAsync();
    }
}
