using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.UseCases.Transaction.QueryGetTransactions
{
    public class GetTransactionsQuery : IRequest<ICollection<TransactionDto>>
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
