using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.UseCases.Customer.QueryGetCustomer
{
    public class GetCustomerQuery : IRequest<CustomerDto>
    {
        public int CustomerId { get; set; }
    }
}
