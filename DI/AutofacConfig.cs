using Autofac.Core;
using Autofac;
using BankServices;
using DataLogic.Data;
using Microsoft.Extensions.Configuration;

namespace DI
{
    public static class AutofacConfig
    {
        public static IContainer Configure(IConfiguration configuration)
        {
            // Create your builder.
            var builder = new ContainerBuilder();

            builder.Register(context => configuration).As<IConfiguration>();
            
            builder.RegisterType<AccountDomain>().As<IAccountDomain>();
            builder.RegisterType<TransactionDomain>().As<ITransactionDomain>();
            builder.RegisterType<XMLAccountRepository>().As<IAccountRepository>();
            builder.RegisterType<TransactionRepository>().As<ITransactionRepository>();

            return builder.Build();
        }
        public static IContainer Configure2()
        {
            // Create your builder.
            var builder = new ContainerBuilder();

            //builder.Register(context => context =>
            //{
            //    var config = context.Resolve<IConfiguration>();
            //    return config.GetSection("DBSettings").Get<IConfiguration>();
            //}).As<IConfiguration>();

            builder.RegisterType<AccountDomain>().As<IAccountDomain>();
            builder.RegisterType<TransactionDomain>().As<ITransactionDomain>();
            builder.RegisterType<XMLAccountRepository>().As<IAccountRepository>();
            builder.RegisterType<TransactionRepository>().As<ITransactionRepository>();

            return builder.Build();
        }

    }
}
