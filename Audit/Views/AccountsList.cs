using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audit.Views
{
    internal static class AccountsList
    {
        public static void Show(ICollection<AccountVM> accounts)
        {
            Console.Clear();
            if (accounts == null || accounts.Count < 1)
            {
                Console.WriteLine("No Accounts in the System");
            }
            else
            {
                int i = 0;
                foreach (var account in accounts)
                {
                    Console.WriteLine($"{++i}. {account.FullName} | Account Number: {account.GetAccountNumber()}");
                }
            }
            Console.ReadKey();
        }
    }
}
