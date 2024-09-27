using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment_4.Views
{
    public static class CreateAccountForm
    {
        public static Customer Show()
        {
            string? firstname, lastname;
            Console.Clear();
            Console.WriteLine("==========================================");
            Console.WriteLine("Enter the Account Holder informations");
            Console.WriteLine("Firstname");
            firstname = Console.ReadLine();
            Console.WriteLine("Lastname");
            lastname = Console.ReadLine();
            return new Customer(firstname, lastname);
        }
    }
}
