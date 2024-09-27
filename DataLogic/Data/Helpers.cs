using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataLogic.Data
{
    public static class Helpers
    {
        public static AccountVM XElementToAccount(XElement xaccount)
        {
            var account = new AccountVM((string)xaccount.Element("AccountNumber"));
            account.FirstName = xaccount.Parent.Parent.Element("FirstName").Value;
            account.LastName = xaccount.Parent.Parent.Element("LastName").Value;
            return account;
        }

        public static Transaction XElementToTransaction(XElement t)
        {
            int transId;
            decimal transAmount;
            DateTime tDate;

            DateTime.TryParse(t.Element("TransactionDate").Value, out tDate);
            int.TryParse(t.Attribute("TransactionId").Value, out transId);
            decimal.TryParse(t.Element("Amount").Value, out transAmount);
            TransactionType transType = (string)t.Element("Type") == "Deposit"
            ? TransactionType.Deposit
            : TransactionType.Withdrawal;
            string DestinationAccountId = (string)t.Element("DestinationAccountId");
            string currency = (string)t.Element("Currency");
            string SourceAccountId = (string)t.Element("SourceAccountId");
            Transaction transaction = DestinationAccountId is null
            ? new Transaction(transId, transAmount, currency, transType)
            : new Transaction(transId, transAmount, currency, transType, SourceAccountId, DestinationAccountId);
            transaction.TransactionDate = tDate;
            return transaction;

        }
    }
}
