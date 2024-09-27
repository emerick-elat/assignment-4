using Autofac;
using BankServices;
using DataLogic.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DI
{
    internal class DIModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Bank>().As<IBank>();
            builder.RegisterType<DataAccess>().As<IDataAccess>();
        }
    }
}
