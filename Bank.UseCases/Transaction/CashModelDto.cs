using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.UseCases.Transaction
{
    public struct CashModelDto
    {
        public int Amount {  get; set; }
        public string AccountNumber { get; set; }
    }
}
