using Bank.UseCases.ScheduledPayment.CommandExecuteScheduledPayment;
using Bank.UseCases.ScheduledPayment.QueryGetScheduledPayments;
using Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.ScheduledTasks
{
    internal class AutomaticPaymentWorker(IMediator _mediator, ILogger<AutomaticPaymentWorker> logger)
        : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var scheduledPayments = await _mediator.Send(new GetScheduledPaymentsQuery());
                    ScheduledPayment? payment = scheduledPayments.FirstOrDefault();
                    if (payment is not null)
                    {
                        var response = await _mediator.Send(new ExecuteScheduledPaymentCommand() 
                        { 
                            AccountNumber = payment.AccountNumber,
                            Amount = payment.Amount,
                        });
                        await Task.Delay(TimeSpan.FromMinutes(payment.Periodicity), stoppingToken);
                        if (!response)
                        {
                            logger.LogWarning($"Payment to of amount {payment.Amount} to account {payment.AccountNumber} failed");
                        }
                    }
                }
                catch (OperationCanceledException)
                {
                    Environment.Exit(1);
                }
                catch (Exception ex)
                {
                    Environment.Exit(1);
                }
            }
        }
    }
}
