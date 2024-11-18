using Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Multitenancy
{
    public delegate void TenantChangedEventHandler(object source, TenantChangedEventArgs args);
    public class TenantService : ITenantService
    {
        private readonly TenantsDBContext _dbContext;
        public TenantService(TenantsDBContext dBContext)
        {
            _dbContext = dBContext;
            _tenant = GetTenant("DefaultConnectionString");
        }

        public TenantService(TenantsDBContext dBContext, string tenant)
        {
            _dbContext = dBContext;
            _tenant = GetTenant(tenant);
        }

        private Tenant _tenant;

        public event TenantChangedEventHandler OnTenantChanged = null!;

        public Tenant Tenant => _tenant;
        
        public void SetTenant(Tenant tenant)
        {
            if (tenant.Name != _tenant.Name)
            {
                var old = _tenant;
                _tenant = tenant;
                OnTenantChanged?.Invoke(this, new TenantChangedEventArgs(old, _tenant));
            }
        }

        public async Task<Tenant[]> GetTenantsAsync() => await _dbContext.Tenants.ToArrayAsync();
        
        public Tenant[] GetTenants() => _dbContext.Tenants.ToArray();
        public Tenant GetTenant(string name) => _dbContext.Tenants.FirstOrDefault(t => t.Name == name)!;
        
    }
}
