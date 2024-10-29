using Bank.UseCases.Account.QueryGetAccounts;
using Bank.UseCases.ScheduledPayment.CommandCreateScheduledPayment;
using Infrastructure.BankAccountRepository;
using Infrastructure.BankAccountRepository.Contract;

namespace Bank.ScheduledTasks
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);
            // Use Autofac as the service provider factory.
            

            builder.Services.AddWindowsService(ws =>
            {
                ws.ServiceName = "SmallBank Scheduled Payments";
            });
            builder.Services.AddScoped<IBankTransactionRepository, BankTransactionRepository>();
            builder.Services.AddHostedService<AutomaticPaymentWorker>();
            builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(CreateScheduledPaymentCommand).Assembly));
            var host = builder.Build();
            host.Run();
        }
    }
}