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

            // Usually you're only interested in exposing the type
            // via its interface:
            builder.RegisterType<Bank>().As<IBank>();
            builder.RegisterType<DataAccess>().As<IDataAccess>();

            // However, if you want BOTH services (not as common)
            // you can say so:
            //builder.RegisterType<SomeType>().AsSelf().As<IService>();

            return builder.Build();
        }

    }
}
