using Autofac;
using Evil.Entity;

namespace Evil.Repository
{
    public class AutofacRepositoryModule : Module
    {
        private const string CustomeFile = "CustomerFile";

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CustomerRepository>().As<ICustomerRepository>().InstancePerLifetimeScope();
            builder.RegisterType<CustomerCsvContext>()
                .As<IDataContext<Customer>>()
                .WithParameter(new NamedParameter("fileKey", CustomeFile))
                .InstancePerLifetimeScope();
        }
    }
}