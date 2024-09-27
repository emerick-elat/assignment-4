using Autofac.Core;
using Autofac;
using BankServices;
using DataLogic.Data;

namespace DI
{
    public static class AutofacConfig
    {
        public static IContainer Configure()
        {
            // Create your builder.
            var builder = new ContainerBuilder();

            
            builder.RegisterType<AccountDomain>().As<IAccountDomain>();
            builder.RegisterType<TransactionDomain>().As<ITransactionDomain>();
            builder.RegisterType<XMLAccountRepository>().As<IAccountRepository>();
            builder.RegisterType<TransactionRepository>().As<ITransactionRepository>();

            return builder.Build();
        }

    }
}
