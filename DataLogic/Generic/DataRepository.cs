using Infrastructure.Context;
using Infrastructure.Generic.Contract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Generic
{
    public class DataRepository<T> : DataRepositoryBase<T>, IDataRepository<T> where T : class
    {
        public DataRepository(BankContext bankContext) : base(bankContext)
        {
        }
        
        public new async Task<bool> EntityExists(Expression<Func<T, bool>> predicate) => await EntityExists(predicate);
        public new async Task<ICollection<T>> GetQueryAsync(Expression<Func<T, bool>> expression)
            => await GetQueryAsync(expression);
        public async Task<ICollection<T>> GetAllAsync()
        {
            return await GetEntitiesAsync();
        }

        public async Task<T> GetEntityByIdAsync(object id)
        {
            return await GetEntityAsync(id);
        }
        
        public async Task<T> CreateEntityAsync(T entity)
        {
            await AddEntityAsync(entity);
            await SaveChangesAsync();
            return entity;
        }

        public async Task UpdateEntityAsync(object key, T entity)
        {
            UpdateEntity(entity);
            await SaveChangesAsync();
        }

        public async Task DeleteEntityAsync(object key)
        {
            var entity = await GetEntityByIdAsync(key);
            DeleteEntity(entity);
            await SaveChangesAsync();
        }
    }
}
