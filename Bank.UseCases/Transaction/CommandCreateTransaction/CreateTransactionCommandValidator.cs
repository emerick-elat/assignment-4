using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.UseCases.Transaction.CommandCreateTransaction
{
    internal class CreateTransactionCommandValidator : AbstractValidator<CreateTransactionCommand>
    {
        public CreateTransactionCommandValidator()
        {
            RuleFor(t => t.SourceAccountId)
                .NotNull().WithMessage("Source Account is required")
                .NotEmpty().WithMessage("Source Account is required");

            RuleFor(t => t.DestinationAccountId)
                .NotNull().WithMessage("Destination Account is required")
                .NotEmpty().WithMessage("Destination Account is required");

            RuleFor(t => t.Amount)
                .NotNull().WithMessage("Amount Account is required")
                .NotEmpty().WithMessage("Amount Account is required")
                .GreaterThan(0).WithMessage("Amount should be greater than 0");
            
            RuleFor(t => t.Type)
                .NotNull().WithMessage("Amount Account is required")
                .NotEmpty().WithMessage("Amount Account is required");

        }
    }
}
