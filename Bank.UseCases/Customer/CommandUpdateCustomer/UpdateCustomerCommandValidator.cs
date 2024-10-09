using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.UseCases.Customer.CommandUpdateCustomer
{
    internal class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
    {
        public UpdateCustomerCommandValidator()
        {
            RuleFor(c => c.FirstName)
                .NotNull().WithMessage("Customer Firstname cannot be null")
                .NotEmpty().WithMessage("Customer Firstname is required")
                .MinimumLength(2)
                .MaximumLength(100);

            RuleFor(c => c.LastName)
                .NotNull().WithMessage("Customer LastName cannot be null")
                .NotEmpty().WithMessage("Customer LastName is required");
        }
    }
}
