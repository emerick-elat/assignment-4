using AutoMapper;
using Bank.UseCases.Customer.CommandCreateCustomer;
using DataLogic.BankAccountRepository.Contract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Bank.UseCases.Customer.CommandUpdateCustomer
{
    internal class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public UpdateCustomerCommandHandler(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task Handle(UpdateCustomerCommand command, CancellationToken cancellationToken)
        {
            var validator = new UpdateCustomerCommandValidator();
            var result = validator.Validate(command);
            if (!result.IsValid)
            {
                throw new ArgumentException(nameof(command));
            }
            var customer = _mapper.Map<Entities.Customer>(command);
            await _customerRepository.UpdateEntityAsync(customer.CustomerId, customer);
        }
    }
}
