﻿using MediatR;
using MediatR.Pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.UseCases.Account.QueryGetAccounts
{
    public class GetCustomerAccountsQuery : IRequest<ICollection<AccountDto>>
    {
        public int CustomerId { get; set; }
    }
}
