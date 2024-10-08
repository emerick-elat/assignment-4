using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.UseCases.Account.QueryGetAccount
{
    public class GetAccountQuery : IRequest<AccountDto>
    {
        public string AccountNumber { get; set; }
    }
}
