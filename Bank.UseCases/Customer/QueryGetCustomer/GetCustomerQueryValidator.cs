using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.UseCases.Customer.QueryGetCustomer
{
    internal class GetCustomerQueryValidator : AbstractValidator<GetCustomerQuery>
    {
        public GetCustomerQueryValidator()
        {
            RuleFor(c => c.CustomerId)
                .NotNull().WithMessage("Customer Id is required")
                .NotEmpty().WithMessage("Customer ID cannot be empty");
        }
    }
}
