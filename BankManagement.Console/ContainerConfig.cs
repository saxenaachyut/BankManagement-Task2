using Autofac;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bank
{
    public static class ContainerConfig
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<AccountHolderServices>().As<IAccountHolderServices>();
            builder.RegisterType<BankServices>().As<IBankServices>();
            builder.RegisterType<TransactionServices>().As<ITransactionServices>();

            return builder.Build();
        }
    }
}
