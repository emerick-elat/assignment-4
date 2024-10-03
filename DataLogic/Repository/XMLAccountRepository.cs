using Entities;
using System.Xml.Linq;

namespace DataLogic.Repository
{
    internal class XMLAccountRepository : IAccountRepository
    {
        private readonly string Database;
        private readonly string SystemAccountNumber;
        private readonly decimal InitialBalance;

        public XMLAccountRepository(IConfig configuration)
        {
            Database = configuration.Path
                ?? throw new ArgumentNullException(nameof(configuration));
            SystemAccountNumber = configuration.SystemAccountNumber
                ?? throw new ArgumentNullException(nameof(configuration));
            InitialBalance = configuration.InitialBalance;
        }

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

                if (doc.Root is not null && SystemAccountNumber.Length == 16)
                {
                    doc.Root.Add(Helpers.CreateSystemAccount(InitialBalance, SystemAccountNumber));
                }
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

                doc.Root?.Add(customerElement);
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

        public ICollection<AccountVM> GetAllAccounts()
        {
            if (File.Exists(Database))
            {
                XDocument doc = XDocument.Load(Database);

                var accounts = doc.Descendants("Accounts").SelectMany(cus => cus.Descendants("Account").Select(a => Helpers.XElementToAccount(a)));

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
                    account.Transactions = xTrans.Descendants("Transaction").Select(Helpers.XElementToTransaction).ToList();
                }
                account.FirstName = accountElement.Parent.Parent.Element("FirstName").Value;
                account.LastName = accountElement.Parent.Parent.Element("LastName").Value;
                return account;
            }
            return null;
            
        }

        public bool CreateInitialBankAccount(decimal amount)
        {
            XDocument doc = File.Exists(Database)
                ? XDocument.Load(Database)
                : new XDocument(new XElement("Bank"));
           
            if (doc.Root is not null && SystemAccountNumber.Length == 16)
            {
                doc.Root.Add(Helpers.CreateSystemAccount(InitialBalance, SystemAccountNumber));
            }
            doc.Save(Database);
            return true;
        }
    }
}
