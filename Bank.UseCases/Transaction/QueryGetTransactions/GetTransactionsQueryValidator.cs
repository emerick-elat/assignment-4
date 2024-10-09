using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.UseCases.Transaction.QueryGetTransactions
{
    internal class GetTransactionsQueryValidator : AbstractValidator<GetTransactionsQuery>
    {
        public GetTransactionsQueryValidator()
        {
            RuleFor(r => r.PageNumber)
                .GreaterThan(0);

            RuleFor(r => r.PageSize)
                .GreaterThanOrEqualTo(30);
        }
    }
}
