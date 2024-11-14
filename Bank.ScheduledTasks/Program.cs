using Bank.UseCases.Account.QueryGetAccounts;
using Bank.UseCases.ScheduledPayment.CommandCreateScheduledPayment;
using Infrastructure.BankAccountRepository;
using Infrastructure.BankAccountRepository.Contract;
using Infrastructure.Context;
using Infrastructure.Multitenancy;
using Microsoft.EntityFrameworkCore;

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
            builder.Services.AddDbContextFactory<BankContext>(opt => { }, ServiceLifetime.Scoped);

            builder.Services.AddScoped<IBankTransactionRepository, BankTransactionRepository>();
            builder.Services.AddScoped<IScheduledPaymentRepository, ScheduledPaymentRepository>();
            builder.Services.AddScoped<ITenantService, TenantService>();
            builder.Services.AddHostedService<AutomaticPaymentWorker>();
            builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(CreateScheduledPaymentCommand).Assembly));
            var host = builder.Build();

            // Create context from each tenant from the Context Factory
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var tenantService = services.GetService<ITenantService>();
                    var factory = services.GetService<IDbContextFactory<BankContext>>();
                    foreach (var tenant in tenantService!.GetTenants())
                    {
                        tenantService.SetTenant(tenant);
                        using var ctx = factory!.CreateDbContext();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while creating the database context: {ex.Message}");
                }
            }
            host.Run();
        }
    }
}