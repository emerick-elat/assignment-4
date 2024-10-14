using FluentValidation;

namespace Bank.UseCases.Customer.CommandCreateCustomer
{
    internal class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerCommandValidator()
        {
            RuleFor(c => c.FirstName)
                .NotNull().WithMessage("Customer Firstname cannot be null")
                .NotEmpty().WithMessage("Customer Firstname is required")
                .MaximumLength(50).WithMessage("Maximum length should be 50");

            RuleFor(c => c.LastName)
                .NotNull().WithMessage("Customer LastName cannot be null")
                .NotEmpty().WithMessage("Customer LastName is required")
                .MaximumLength(50).WithMessage("Maximum length should be 50");

            RuleFor(c => c.Email)
                .EmailAddress().WithMessage("Invalid EMail address")
                .NotNull().WithMessage("Email cannot be null")
                .NotEmpty().WithMessage("Email is required")
                .MaximumLength(15).WithMessage("Maximum length of Email should be 15");

            RuleFor(c => c.PhoneNumber)
                .NotNull().WithMessage("PhoneNumber cannot be null")
                .NotEmpty().WithMessage("PhoneNumber is required")
                .MaximumLength(30).WithMessage("Maximum length of PhoneNumber should be 30");
        }
    }
}
