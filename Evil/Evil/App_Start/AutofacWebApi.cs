using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;

namespace Evil
{
    public static class AutofacWebApi
    {
        public static void RegisterResolver()
        {
            var resolver = new AutofacWebApiDependencyResolver(RegisterServices(new ContainerBuilder()));

            // Configure Web API with the dependency resolver.
            GlobalConfiguration.Configuration.DependencyResolver = resolver;
        }

        private static IContainer RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterModule(new AutofacApiModule());
            var container = builder.Build();

            return container;
        }
    }
}