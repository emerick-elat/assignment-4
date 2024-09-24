using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment_4.Views
{
    public static class SearchAccountForm
    {
        public static string Show()
        {
            string? accountNumber;
            Console.WriteLine("Please write account number");
            do
            {
                accountNumber = Console.ReadLine();
            } while (accountNumber is null);
            return accountNumber;
        }
    }
}
