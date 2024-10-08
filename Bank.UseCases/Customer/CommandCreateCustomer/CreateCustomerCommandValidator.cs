﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.UseCases.Customer.CommandCreateCustomer
{
    internal class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerCommandValidator()
        {
            RuleFor(c => c.FirstName)
                .NotNull().WithMessage("Customer Firstname cannot be null")
                .NotEmpty().WithMessage("Customer Firstname is required");

            RuleFor(c => c.LastName)
                .NotNull().WithMessage("Customer LastName cannot be null")
                .NotEmpty().WithMessage("Customer LastName is required");
        }
    }
}