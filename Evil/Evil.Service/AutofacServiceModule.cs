using Autofac;
using Evil.Repository;

namespace Evil.Service
{
    public class AutofacServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CustomerService>().As<ICustomerService>().InstancePerLifetimeScope();
            builder.RegisterModule(new AutofacRepositoryModule());
        }
    }
}