using DataLogic.BankAccountRepository.Contract;
using DataLogic.Context;
using DataLogic.Generic;
using DataLogic.Generic.Contract;
using DataLogic.Repository;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DataLogic.BankAccountRepository
{
    public class BankTransactionRepository : DataRepository<Transaction>, IBankTransactionRepository
    {
        private readonly IBankAccountRepository _accountRepo;
        private readonly ILogger _logger;
        private readonly string SystemAccountNumber;
        public BankTransactionRepository(BankContext bankContext,
            IBankAccountRepository accountRepository,
            ILogger logger,
            IConfiguration configuration) : base(bankContext)
        {
            _accountRepo = accountRepository 
                ?? throw new ArgumentNullException(nameof(accountRepository));
            _logger = logger 
                ?? throw new ArgumentNullException(nameof(logger));
            SystemAccountNumber = configuration["SystemAccountNumber"] 
                ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<bool> DepositMoney(Transaction t)
            => await CreateTransaction(t);
        public async Task<bool> WithdrawMoney(Transaction t)
            => await CreateTransaction(t);
        public async Task<bool> TransferMoney(Transaction t)
            => await CreateTransaction(t);
        public async Task<ICollection<Transaction>> GetTransactionsHistory(string? accountNumber = null, DateTime? Start = null, DateTime? End = null)
        {
            bool accountExists = await _accountRepo.EntityExists(a => a.AccountNumber == accountNumber);
            if (accountNumber is not null && !accountExists)
            {
                _logger.LogWarning("Account do not exists");
                throw new ArgumentException("Account do not exists", nameof(accountNumber));
            }
            if (Start is null && End is null)
            {
                return [];
                //return _transactionRepository.GetTransactionsHistory(accountNumber);
            }
            else
            {
                return [];
                //return _transactionRepository.GetTransactionsHistory(accountNumber, Start, End);
            }
        }
        public async Task<bool> CreateTransaction(Transaction t)
        {   if (t is null)
            {
                throw new ArgumentNullException(nameof(t));
            }
            t.SourceAccountId = t.SourceAccountId.Trim().Replace(" ", "") ?? string.Empty;
            Account sourceAccount = await _accountRepo.GetEntityByIdAsync(t.SourceAccountId);

            if (sourceAccount is null)
            {
                _logger.LogWarning($"Transaction from account: {t.SourceAccountId} don't exists ");
                throw new ArgumentNullException(nameof(sourceAccount));
            }

            if (sourceAccount.GetBalance() < t.Amount)
            {
                _logger.LogWarning($"Transaction from account: {t.SourceAccountId} failed, Insufficient funds ");
                return false;
            }

            if (t.DestinationAccountId is not null)
            {
                t.DestinationAccountId = t.DestinationAccountId.Trim().Replace(" ", "");
                if (t.SourceAccountId.Equals(t.DestinationAccountId))
                {
                    _logger.LogWarning($"Transaction from then same account account: {t.SourceAccountId} ");
                    throw new ArgumentException("Transaction on the same account", nameof(t.DestinationAccountId));
                }
                var destinationAccount = _accountRepo.GetEntityByIdAsync(t.DestinationAccountId);

                if (destinationAccount is null)
                {
                    _logger.LogWarning($"Transaction To account: {t.DestinationAccountId} don't exists ");
                    throw new ArgumentNullException(nameof(destinationAccount));
                }
            }
            var result = await CreateEntityAsync(t);
            return result == null ? false : true;
        }
    }
}
