using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.UseCases.Customer.CommandCreateCustomer
{
    public class CreateCustomerCommand : IRequest<CustomerDto>
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
    }
}
