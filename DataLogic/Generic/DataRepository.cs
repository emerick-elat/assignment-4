using DataLogic.Context;
using DataLogic.Generic.Contract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLogic.Generic
{
    public class DataRepository<T> : DataRepositoryBase<T>, IDataRepository<T> where T : class
    {
        public DataRepository(BankContext bankContext) : base(bankContext)
        {
        }

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
