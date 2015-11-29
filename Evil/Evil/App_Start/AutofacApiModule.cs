using System.Reflection;
using Autofac;
using Autofac.Integration.WebApi;
using Evil.Service;
using Module = Autofac.Module;

namespace Evil
{
    public class AutofacApiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly()).PropertiesAutowired();
            builder.RegisterTypes().PropertiesAutowired();
            builder.RegisterModule(new AutofacServiceModule());
        }
    }
}