
using Bank.UseCases.Account.QueryGetAccounts;
using Infrastructure.BankAccountRepository.Contract;
using Infrastructure.BankAccountRepository;
using Infrastructure.Context;
using Infrastructure.Generic.Contract;
using Infrastructure.Generic;
using Bank.AuditAPI.Profiles;
using Microsoft.EntityFrameworkCore;

namespace Bank.AuditAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddDbContext<BankContext>();
            builder.Services.AddAutoMapper(typeof(TransactionProfile));
            builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(GetCustomerAccountsQueryHandler).Assembly));

            builder.Services.AddScoped(typeof(IDataRepository<>), typeof(DataRepository<>));
            builder.Services.AddScoped(typeof(IDataRepositoryBase<>), typeof(DataRepositoryBase<>));
            builder.Services.AddScoped<IBankAccountRepository, BankAccountRepository>();
            builder.Services.AddScoped<IBankTransactionRepository, BankTransactionRepository>();
            builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<BankContext>();
                    context.Database.Migrate();
                }
                catch (Exception ex)
                {
                    // Handle exceptions if needed
                    Console.WriteLine($"An error occurred while migrating the database: {ex.Message}");
                }
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
