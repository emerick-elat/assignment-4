using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Multitenancy
{
    public interface ITenantService
    {
        Tenant Tenant { get; }
        void SetTenant(Tenant tenant);
        Tenant[] GetTenants();
        Tenant GetTenant(string name);

        event TenantChangedEventHandler OnTenantChanged;
    }
}
