using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Multitenancy
{
    public class TenantChangedEventArgs : EventArgs
    {
        public TenantChangedEventArgs(string? oldTenant, string newTenant)
        {
            OldTenant = oldTenant;
            NewTenant = newTenant;
        }

        public string? OldTenant { get; private set; }
        public string NewTenant { get; private set; }
    }
}
