using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataLogic.Data
{
    internal class TransactionRepository : ITransactionRepository
    {
        private readonly string Database = _DB.Path;
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
                else
                {
                    xtransactions = doc.Descendants("Transactions");
                }

                if (xtransactions.Descendants().Any())
                {
                    transactions = xtransactions.Descendants("Transaction").Select(Helpers.XElementToTransaction).ToList();
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
