using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.UseCases.Account
{
    public class AccountDto
    {
        public string AccountNumber { get; set; }
        public string CustomerFullname { get; set; }
        public string Balance { get; set; }
    }
}
