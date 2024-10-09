using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.UseCases.Customer.QueryGetCustomers
{
    public class GetCustomersQuery : IRequest<ICollection<CustomerDto>>
    {
        public int PageSize {  get; set; }
        public int PageNumber { get; set; }
    }
}
