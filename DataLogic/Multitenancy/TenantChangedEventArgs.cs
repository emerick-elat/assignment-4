using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Multitenancy
{
    public class TenantChangedEventArgs : EventArgs
    {
        public TenantChangedEventArgs(Tenant oldTenant, Tenant newTenant)
        {
            OldTenant = oldTenant;
            NewTenant = newTenant;
        }

        public Tenant? OldTenant { get; private set; }
        public Tenant NewTenant { get; private set; }
    }
}
