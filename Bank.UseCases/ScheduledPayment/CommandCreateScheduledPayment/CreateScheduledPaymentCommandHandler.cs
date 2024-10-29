using AutoMapper;
using Infrastructure.BankAccountRepository.Contract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.UseCases.ScheduledPayment.CommandCreateScheduledPayment
{
    internal class CreateScheduledPaymentCommandHandler(IScheduledPaymentRepository _repo, IMapper _mapper)
        : IRequestHandler<CreateScheduledPaymentCommand, Entities.ScheduledPayment>
    {
        public async Task<Entities.ScheduledPayment> Handle(CreateScheduledPaymentCommand request, CancellationToken cancellationToken)
        {
            CreateScheduledPaymentCommandValidator validator = new CreateScheduledPaymentCommandValidator();
            Entities.ScheduledPayment scheduledPayment = _mapper.Map<Entities.ScheduledPayment>(request);
            var result = validator.Validate(scheduledPayment);
            if (!result.IsValid)
            {
                throw new ArgumentException(nameof(scheduledPayment), "Validation error");
            }
            await _repo.CreateEntityAsync(scheduledPayment);
            return scheduledPayment;
        }
    }
}
