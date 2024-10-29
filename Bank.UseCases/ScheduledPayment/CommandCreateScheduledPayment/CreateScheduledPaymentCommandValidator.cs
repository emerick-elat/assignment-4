using FluentValidation;

namespace Bank.UseCases.ScheduledPayment.CommandCreateScheduledPayment
{
    public class CreateScheduledPaymentCommandValidator : AbstractValidator<Entities.ScheduledPayment>
    {
        public CreateScheduledPaymentCommandValidator()
        {
            RuleFor(sp => sp.AccountNumber)
                .NotNull()
                .NotEmpty()
                .Length(16);
            RuleFor(sp => sp.Amount)
                .NotNull()
                .GreaterThan(0)
                .LessThan(10000);
            RuleFor(sp => sp.Periodicity)
                .NotEmpty()
                .NotNull()
                .GreaterThan(0).WithMessage("")
                .LessThan(1000);
        }
    }
}
