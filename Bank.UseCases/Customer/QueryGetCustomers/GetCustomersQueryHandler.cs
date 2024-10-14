using AutoMapper;
using Infrastructure.BankAccountRepository.Contract;
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

        public GetCustomersQueryHandler(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<ICollection<CustomerDto>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
        {
            var customers = (request?.PageNumber != 0 && request?.PageSize != 0) 
                ? await _customerRepository.GetPaginatedResults(request.PageSize, request.PageNumber)
                : await _customerRepository.GetAllAsync();
            return _mapper.Map<ICollection<CustomerDto>>(customers);
        }
    }
}
