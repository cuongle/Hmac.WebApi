using System.Linq;
using System.Reflection;
using Autofac;
using Module = Autofac.Module;

namespace Hmac.Application
{
    public class ApplicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var serviceTypes = Assembly.GetExecutingAssembly()
                                 .GetTypes()
                                 .Where(type => type.Name.EndsWith("Service") && !type.IsInterface);

            foreach (var type in serviceTypes)
            {
                builder.RegisterType(type)
                        .AsImplementedInterfaces()
                        .InstancePerLifetimeScope();
            }
        }
    }
}

