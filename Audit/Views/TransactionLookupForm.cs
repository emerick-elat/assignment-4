using BankServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audit.Views
{
    public static class TransactionLookupForm
    {
        public static (string?, DateRange?) Show()
        {
            DateTime startDate;
            DateTime endDate;
            string? accountNumber, startDateStr = string.Empty, endDateStr = string.Empty;
            Console.Clear();
            Console.WriteLine("Account Number, Leave empty to show all accounts");
            accountNumber = Console.ReadLine();
            Console.WriteLine("Enter Date range in the format: yyyy-mm-dd");
            Console.WriteLine("Start Date:");
            startDateStr = Console.ReadLine();
            bool isValidStart = DateTime.TryParse(startDateStr, out startDate);
            Console.WriteLine("End Date:");
            endDateStr = Console.ReadLine();
            bool isValidEnd = DateTime.TryParse(endDateStr, out endDate);
            accountNumber = string.IsNullOrEmpty(accountNumber) ? null : accountNumber;
            return (isValidStart && isValidEnd) ? (accountNumber, new DateRange()
            {
                Start = startDate,
                End = endDate
            }) : (accountNumber, null);
        }
    }
}
