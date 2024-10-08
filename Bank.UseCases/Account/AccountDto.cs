using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.UseCases.Account
{
    public class AccountDto
    {
        public int Id { get; set; }
        public int CustomerFullname { get; set; }
        public string AccountNumber { get; set; }
    }
}
