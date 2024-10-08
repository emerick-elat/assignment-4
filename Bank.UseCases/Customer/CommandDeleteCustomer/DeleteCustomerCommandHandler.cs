using DataLogic.BankAccountRepository.Contract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.UseCases.Customer.CommandDeleteCustomer
{
    internal class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand>
    {
        private readonly ICustomerRepository _customerRepository;

        public DeleteCustomerCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var validator = new DeleteCustomerValidator();
            var result = validator.Validate(request);
            if (!result.IsValid) { 
                throw new ArgumentException("Customer ID is requested", nameof(request));
            }

            await _customerRepository.DeleteEntityAsync(request.CustomerId);
        }
    }
}
