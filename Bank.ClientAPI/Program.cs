
using Bank.ClientAPI.Profiles;
using Bank.UseCases.Account.QueryGetAccounts;
using DataLogic.BankAccountRepository;
using DataLogic.BankAccountRepository.Contract;
using DataLogic.Generic;
using DataLogic.Generic.Contract;
using DataLogic.Repository;

namespace Bank.ClientAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddAutoMapper(typeof(AccountProfile));
            builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(GetAccountsQueryHandler).Assembly));

            builder.Services.AddScoped(typeof(IDataRepository<>), typeof(DataRepository<>));
            builder.Services.AddScoped(typeof(IDataRepositoryBase<>), typeof(DataRepositoryBase<>));
            builder.Services.AddScoped<IBankAccountRepository, BankAccountRepository>();
            builder.Services.AddScoped<IBankTransactionRepository, BankTransactionRepository>();
            builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

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
