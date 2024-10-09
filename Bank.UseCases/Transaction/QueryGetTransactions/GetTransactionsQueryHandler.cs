using AutoMapper;
using DataLogic.BankAccountRepository.Contract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.UseCases.Transaction.QueryGetTransactions
{
    public class GetTransactionsQueryHandler : IRequestHandler<GetTransactionsQuery, ICollection<TransactionDto>>
    {
        private readonly IBankTransactionRepository _transactionRepository;
        private readonly IMapper _mapper;
        public GetTransactionsQueryHandler(IBankTransactionRepository transactionRepository, IMapper mapper)
        {
            _mapper = mapper;
            _transactionRepository = transactionRepository;
        }
        public async Task<ICollection<TransactionDto>> Handle(GetTransactionsQuery request, CancellationToken cancellationToken)
        {
            var transactions = _transactionRepository.GetTransactionsHistory();
            return _mapper.Map<List<TransactionDto>>(transactions);
        }
    }
}
