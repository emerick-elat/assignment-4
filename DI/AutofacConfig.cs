﻿using Autofac.Core;
using Autofac;
using BankServices;
using DataLogic.Data;
using Microsoft.Extensions.Configuration;
using Entities;

namespace DI
{
    public static class AutofacConfig
    {
        public static IContainer Configure(IConfiguration configuration)
        {
            var config = configuration.GetSection("DBSettings").Get<Config>()
                ?? throw new ArgumentNullException(nameof(configuration));
            // Create your builder.
            var builder = new ContainerBuilder();

            builder.Register(context => config).As<IConfig>();
            
            builder.RegisterType<AccountDomain>().As<IAccountDomain>();
            builder.RegisterType<TransactionDomain>().As<ITransactionDomain>();
            builder.RegisterType<XMLAccountRepository>().As<IAccountRepository>();
            builder.RegisterType<TransactionRepository>().As<ITransactionRepository>();

            return builder.Build();
        }
        
    }
}
