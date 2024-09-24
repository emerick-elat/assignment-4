using BankServices;
using DataLogic.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audit.Views
{
    public static class AccountView
    {
        public static void Show(AccountVM ? account)
        {
            ServiceProvider provider = new ServiceCollection().AddSingleton<IBank, Bank>().BuildServiceProvider();
            IBank bank = provider.GetService<IBank>()
                ??throw new ArgumentNullException(nameof(bank));

            Console.Clear();
            if (account is not null)
            {
                
            }
            else
            {
                Console.WriteLine("Account Not found");
                Console.ReadKey();
            }

        }
    }
}
