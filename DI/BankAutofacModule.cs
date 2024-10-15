using Autofac;
using Infrastructure.BankAccountRepository;
using Infrastructure.BankAccountRepository.Contract;
using Infrastructure.Context;
using Infrastructure.Generic;
using Infrastructure.Generic.Contract;
using Infrastructure.Repository;

namespace DI
{
    public class BankAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Register your services here
            //builder.RegisterType<BankContext>().AsSelf().InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(DataRepositoryBase<>)).As(typeof(IDataRepositoryBase<>));
            builder.RegisterGeneric(typeof(DataRepository<>)).As(typeof(IDataRepository<>));
            builder.RegisterType<BankAccountRepository>().As<IBankAccountRepository>();
            builder.RegisterType<TransactionRepository>().As<ITransactionRepository>();
            builder.RegisterType<CustomerRepository>().As<ICustomerRepository>();
        }
    }
}
