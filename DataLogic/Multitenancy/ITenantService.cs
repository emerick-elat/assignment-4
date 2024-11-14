using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Multitenancy
{
    public interface ITenantService
    {
        string Tenant { get; }

        void SetTenant(string tenant);

        string[] GetTenants();

        event TenantChangedEventHandler OnTenantChanged;
    }
}
