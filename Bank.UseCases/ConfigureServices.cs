using Infrastructure.BankAccountRepository.Contract;
using Infrastructure.BankAccountRepository;
using Infrastructure.Generic.Contract;
using Infrastructure.Generic;
using Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;
using Bank.UseCases.Account.QueryGetAccounts;
using Infrastructure.Context;
using Microsoft.AspNetCore.Identity;

namespace Bank.UseCases
{
    public static class ConfigureServices
    {
        public static void ConfigureDependencies(this IServiceCollection services)
        {
            services.AddScoped(typeof(IDataRepository<>), typeof(DataRepository<>));
            services.AddScoped(typeof(IDataRepositoryBase<>), typeof(DataRepositoryBase<>));
            services.AddScoped<IBankAccountRepository, BankAccountRepository>();
            services.AddScoped<IBankTransactionRepository, BankTransactionRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
        }
    }
}
