using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.UseCases.Customer.CommandDeleteCustomer
{
    internal class DeleteCustomerValidator : AbstractValidator<DeleteCustomerCommand>
    {
        public DeleteCustomerValidator()
        {
            RuleFor(c => c.CustomerId)
                .NotNull().WithMessage("Customer Id is required")
                .NotEmpty().WithMessage("Customer Id is required");
        }
    }
}
