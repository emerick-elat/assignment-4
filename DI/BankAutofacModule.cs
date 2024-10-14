using Autofac;
using AutoMapper;
using Bank.UseCases.Account.CommandCreateAccount;
using Infrastructure.BankAccountRepository;
using Infrastructure.BankAccountRepository.Contract;
using Infrastructure.Context;
using Infrastructure.Generic;
using Infrastructure.Generic.Contract;
using Infrastructure.Repository;
using MediatR.Extensions.Autofac.DependencyInjection;
using MediatR.Extensions.Autofac.DependencyInjection.Builder;

namespace DI
{
    public class BankAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Register your services here
            builder.RegisterType<BankContext>().InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(DataRepositoryBase<>)).As(typeof(IDataRepositoryBase<>));
            builder.RegisterGeneric(typeof(DataRepository<>)).As(typeof(IDataRepository<>));
            builder.RegisterType<BankAccountRepository>().As<IBankAccountRepository>();
            builder.RegisterType<TransactionRepository>().As<ITransactionRepository>();
            builder.RegisterType<CustomerRepository>().As<ICustomerRepository>();

            //builder.RegisterMediatR(MediatRConfigurationBuilder
            //        .Create(AppDomain.CurrentDomain.GetAssemblies())
            //        .WithAllOpenGenericHandlerTypesRegistered()
            //        .Build());

            var configuration = MediatRConfigurationBuilder
                .Create(typeof(CreateAccountCommand).Assembly)
                .WithAllOpenGenericHandlerTypesRegistered()
                .WithRegistrationScope(RegistrationScope.Scoped) // currently only supported values are `Transient` and `Scoped`
                .Build();

            builder.RegisterMediatR(configuration);

            builder.RegisterType<MapperProfile>().As<Profile>();
            builder.Register(context =>
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<MapperProfile>();
                });

                return config.CreateMapper();
            }).As<IMapper>().InstancePerLifetimeScope();


            // Register AutoMapper profiles
            //builder.Register(ctx => new MapperConfiguration(cfg =>
            //{
            //    cfg.AddProfile<Program>();
            //    // Add other profiles here
            //})).AsSelf().SingleInstance();

            // Register IMapper
            //builder.Register(ctx => ctx.Resolve<MapperConfiguration>().CreateMapper()).As<IMapper>().InstancePerLifetimeScope();
        }
    }
}
