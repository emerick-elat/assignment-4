using DataLogic.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLogic.Generic.Contract
{
    public interface IDataRepositoryBase<T> where T : class
    {
        Task<ICollection<T>> GetEntitiesAsync();
        Task<T> GetEntityAsync(object id);
        Task AddEntityAsync(T entity);
        void UpdateEntity(T entity);
        void DeleteEntity(T entity);
        Task SaveChangesAsync();
    }
}
