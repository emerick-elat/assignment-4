﻿using DataLogic.Context;
using DataLogic.Generic.Contract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLogic.Generic
{
    public class DataRepositoryBase<T> : IDataRepositoryBase<T> where T : class
    {
        private readonly BankContext bankContext;
        private readonly DbSet<T> dbSet;

        public DataRepositoryBase(BankContext bankContext, DbSet<T> dbSet)
        {
            this.bankContext = bankContext;
            this.dbSet = dbSet;
        }

        public async Task<ICollection<T>> GetEntitiesAsync()
        {
            return await dbSet.ToListAsync();
        }

        public async Task<T> GetEntityAsync(object id)
        {
            return await dbSet.FindAsync(id);
        }

        public async Task AddEntityAsync(T entity)
        {
            await bankContext.AddAsync(entity);
        }

        public void UpdateEntity(T entity)
        {
            dbSet.Update(entity);
        }

        public void DeleteEntity(T entity)
        {
            dbSet.Remove(entity);
        }

        public async Task SaveChangesAsync()
        {
            await bankContext.SaveChangesAsync();
        }
    }
}
