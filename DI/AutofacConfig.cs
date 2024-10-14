using Autofac;
using Microsoft.Extensions.Configuration;

namespace DI
{
    public static class AutofacConfig
    {
        public static IContainer Configure(IConfiguration configure)
        {
            //var config = configuration.GetSection("DBSettings").Get<Config>() ?? throw new ArgumentNullException(nameof(configuration));
            // Create your builder.
            var builder = new ContainerBuilder();

            //builder.Register(context => config).As<IConfig>();
            //builder.Register(context => configuration).As<IConfiguration>();

            //builder.RegisterType<BankContext>().As<IBankContext>();
            //builder.RegisterType<AccountDomain>().As<IAccountDomain>();
            //builder.RegisterType<TransactionDomain>().As<ITransactionDomain>();

            //builder.RegisterType<DBAccountRepository>().As<IAccountRepository>();
            //builder.RegisterType<DBTransactionRepository>().As<ITransactionRepository>();
            //builder.RegisterType<XMLAccountRepository>().As<IAccountRepository>();
            //builder.RegisterType<TransactionRepository>().As<ITransactionRepository>();
            
            
            
            return builder.Build();
        }
        
    }
}
