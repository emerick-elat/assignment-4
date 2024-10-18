using Autofac;
using Entities;
using Infrastructure.BankAccountRepository;
using Infrastructure.BankAccountRepository.Contract;
using Infrastructure.Context;
using Infrastructure.Generic;
using Infrastructure.Generic.Contract;
using Infrastructure.Identity;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DI
{
    public class BankAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Register your services here
            //builder.RegisterType<BankContext>().AsSelf().InstancePerLifetimeScope();
            //builder.RegisterGeneric(typeof(IdentityDbContext<>)).AsSelf().InstancePerLifetimeScope();
            //builder.RegisterGeneric(typeof(IdentityDbContext<>)).AsSelf().InstancePerLifetimeScope();
            

            builder.RegisterType<CustomerStore>().As<IUserStore<Customer>>();
            builder.RegisterGeneric(typeof(DataRepositoryBase<>)).As(typeof(IDataRepositoryBase<>));
            builder.RegisterGeneric(typeof(DataRepository<>)).As(typeof(IDataRepository<>));
            builder.RegisterType<BankAccountRepository>().As<IBankAccountRepository>();
            builder.RegisterType<TransactionRepository>().As<ITransactionRepository>();
            builder.RegisterType<CustomerRepository>().As<ICustomerRepository>();
            builder.RegisterType<BankRoleRepository>().As<IBankRoleRepository>();

            builder.RegisterType<CustomerStore>().As<IUserStore<Customer>>();
            builder.RegisterType<BankRoleStore>().As<IRoleStore<BankRole>>();
        }
    }
}
