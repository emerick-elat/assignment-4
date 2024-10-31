using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class CustomerBankRole
    {
        public int CustomerId {  get; set; }
        public Customer? Customer { get; set; }
        public int BankRoleId {  get; set; }
        public BankRole? BankRole { get; set; }
    }
}
