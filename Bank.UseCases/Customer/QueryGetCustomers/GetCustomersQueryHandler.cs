using AutoMapper;
using DataLogic.BankAccountRepository.Contract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.UseCases.Customer.QueryGetCustomers
{
    public class GetCustomersQueryHandler : IRequestHandler<GetCustomersQuery, ICollection<CustomerDto>>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        public async Task<ICollection<CustomerDto>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
        {
            var customers = await _customerRepository.GetAllAsync();
            return _mapper.Map<ICollection<CustomerDto>>(customers);
        }
    }
}
