using Autofac;
using Autofac.Integration.Mvc;
using Microsoft.Owin;
using Owin;
using System;
using System.Reflection;
using System.Web.Mvc;

[assembly: OwinStartupAttribute(typeof(AutofacDemo.Startup))]
namespace AutofacDemo
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            IContainer container = null;
            var builder = new ContainerBuilder();

            // Register Services
            builder.RegisterType<SalesRepository>().As<ISalesRepository>().InstancePerRequest();
            builder.RegisterType<SalesService>().As<ISalesService>().InstancePerRequest();
            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
               .AsClosedTypesOf(typeof(IHandle<>))
               .AsImplementedInterfaces()
               .InstancePerRequest();
            builder.Register<IAppEvents>(_ => new AppEvents(container)).InstancePerRequest();
            
            // Register MVC Controllers
            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            // Resolve dependencies
            container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            app.UseAutofacMiddleware(container);
            app.UseAutofacMvc();
        }
    }
}
