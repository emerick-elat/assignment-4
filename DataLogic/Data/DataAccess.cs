using DataLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DataLogic.Data
{
    public class DataAccess : IDataAccess
    {
        private readonly string Database = @"D://bankData.xml";

        public Account? CreateBankAccount(Customer customer)
        {
            XDocument doc;
            
            if (File.Exists(Database))
            {
                doc = XDocument.Load(Database);
            }
            else
            {
                doc = new XDocument(new XElement("Bank"));
            }
            Account? account = customer.Accounts.FirstOrDefault();
            if (account is not null)
            {
                int CustomerId = doc.Descendants().Count() + 1;
                XElement customerElement = new XElement("Customer",
                new XAttribute("Id", CustomerId),
                new XElement("CustomerId", CustomerId),
                new XElement("FirstName", customer.FirstName),
                new XElement("LastName", customer.LastName),
                new XElement("Accounts",
                    new XElement("Account",
                        new XAttribute("AccountId", account.AccountNumber),
                        new XElement("AccountNumber", account.AccountNumber),
                        new XElement("Transactions"))
                    )
                );

                doc.Root.Add(customerElement);
                doc.Save(Database);
            }
            return account;
        }

        public void DeleteAccount(string accountId)
        {
            if (File.Exists(Database))
            {
                XDocument doc = XDocument.Load(Database);

                // Find the account element with the specified account ID
                XElement accountElement = doc.Descendants("Account").FirstOrDefault(a => (string)a.Attribute("AccountId") == accountId);

                if (accountElement != null)
                {
                    accountElement.Remove();
                    doc.Save(Database);
                    Console.WriteLine("Account deleted successfully.");
                }
                else
                {
                    Console.WriteLine("Account not found.");
                }
            }
        }

        public bool AddTransaction(decimal amount, TransactionType type, string SourceAccountId, string? DestinationAccountId = null)
        {
            if (File.Exists(Database))
            {
                XDocument doc = XDocument.Load(Database);

                int transactionId = doc.Descendants().Count() + 1;
                XElement accountElement = doc.Descendants("Account").FirstOrDefault(a => (string)a.Attribute("AccountId") == SourceAccountId);

                if (accountElement is not null)
                {
                    XElement transactionElement = new XElement("Transaction",
                        new XAttribute("TransactionId", transactionId),
                        new XElement("TransactionId", transactionId),
                        new XElement("Currency", "EUR"),
                        new XElement("Amount", amount),
                        new XElement("Type", type),
                        new XElement("TransactionDate", DateTime.Now.ToString()),
                        new XElement("SourceAccountId", SourceAccountId),
                        new XElement("DestinationAccountId", SourceAccountId)
                    );

                    if (DestinationAccountId is not null)
                    {
                        DestinationAccountId = DestinationAccountId.Replace(" ", "");
                        XElement accountElementTo = doc.Descendants("Account").FirstOrDefault(a => (string)a.Attribute("AccountId") == DestinationAccountId);
                        XElement transactionElementTo = new XElement("Transaction",
                            new XAttribute("TransactionId", transactionId),
                            new XElement("TransactionId", transactionId),
                            new XElement("Currency", "EUR"),
                            new XElement("Amount", amount),
                            new XElement("Type", "Deposit"),
                            new XElement("TransactionDate", DateTime.Now.ToString()),
                            new XElement("SourceAccountId", SourceAccountId),
                            new XElement("DestinationAccountId", DestinationAccountId)
                        );
                        accountElementTo?.Element("Transactions")?.Add(transactionElementTo);
                    }
                    
                    accountElement.Element("Transactions")?.Add(transactionElement);
                    doc.Save(Database);
                    Console.WriteLine("------- OPERATION WAS SUCCESSFULL ---------");
                    return true;
                }
            }
            Console.WriteLine("!!! An Error Occured !!!");
            return false;
        }

        public List<AccountVM> GetAllAccounts()
        {
            if (File.Exists(Database))
            {
                XDocument doc = XDocument.Load(Database);

                var accounts = doc.Descendants("Accounts").SelectMany(cus => cus.Descendants("Account").Select(a => XElementToAccount(a)));

                return accounts.ToList();
            }
            return new List<AccountVM>();
        }

        public bool BankAccountExists(string accountId)
        {   
            if (File.Exists(Database))
            {
                accountId = accountId.Replace(" ", "");
                XDocument doc = XDocument.Load(Database);
                XElement accountElement = doc.Descendants("Account").FirstOrDefault(a => (string)a.Attribute("AccountId") == accountId);
                return accountElement is not null;
            }
            return false;
        }

        public AccountVM? GetBankAccount(string accountId)
        {
            accountId = accountId.Replace(" ", "");
            decimal transAmount;
            int transId = 0;
            AccountVM account = new AccountVM(accountId);
            if (File.Exists(Database))
            {
                XDocument doc = XDocument.Load(Database);
                XElement accountElement = doc.Descendants("Account").FirstOrDefault(a => (string)a.Attribute("AccountId") == accountId);
                if (accountElement is null) return null;
                var xTrans = accountElement.Descendants("Transactions");
                if (!xTrans.Descendants().Any())
                {
                    account.Transactions = [];
                }
                else {
                    account.Transactions = xTrans.Descendants("Transaction").Select(XElementToTransaction).ToList();
                }
                account.FirstName = accountElement.Parent.Parent.Element("FirstName").Value;
                account.LastName = accountElement.Parent.Parent.Element("LastName").Value;
                return account;
            }
            return null;
            
        }

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
        public List<Transaction> GetTransactionsHistory(string? accountNumber = null, DateTime? start = null, DateTime? end = null)
        {
            List<Transaction> transactions = new List<Transaction>();
            if (File.Exists(Database))
            {
                XDocument doc = XDocument.Load(Database);
                XElement xelement;
                IEnumerable<XElement> xtransactions;
                if (accountNumber is not null)
                {
                    accountNumber = accountNumber.Replace(" ", "");
                    xelement = doc.Descendants("Account").FirstOrDefault(a => (string)a.Attribute("AccountId") == accountNumber);
                    xtransactions = xelement.Descendants("Transactions");
                }
                else {
                    xtransactions = doc.Descendants("Transactions");
                }
                
                if (xtransactions.Descendants().Any())
                {
                    transactions = xtransactions.Descendants("Transaction").Select(XElementToTransaction).ToList();
                }
                if (start is not null && end is not null)
                {
                    transactions = transactions.Where(t => t.TransactionDate >= start && t.TransactionDate <= end).ToList();
                }
                
            }
            return transactions;
        }
    }
}
