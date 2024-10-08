using AutoMapper;
using DataLogic.BankAccountRepository.Contract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.UseCases.Customer.CommandCreateCustomer
{
    internal class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, CustomerDto>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CreateCustomerCommandHandler(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<CustomerDto> Handle(CreateCustomerCommand command, CancellationToken cancellationToken)
        {
            var validator = new CreateCustomerCommandValidator();
            var result = validator.Validate(command);
            if (!result.IsValid) {
                throw new ArgumentException(nameof(command));
            }
            var customer = _mapper.Map<Entities.Customer>(command);
            var response = _customerRepository.CreateEntityAsync(customer);
            return _mapper.Map<CustomerDto>(response);
            
        }
    }
}
