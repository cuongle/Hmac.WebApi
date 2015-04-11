using System.Linq;
using System.Reflection;
using Autofac;
using Module = Autofac.Module;

namespace Hmac.Data
{
    public class DataModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var repositoryTypes = Assembly.GetExecutingAssembly()
                                   .GetTypes()
                                   .Where(type => type.Name.EndsWith("Repository") && !type.IsInterface);

            foreach (var type in repositoryTypes)
            {
                builder.RegisterType(type)
                        .AsImplementedInterfaces()
                        .InstancePerLifetimeScope();
            }
        }
    }
}

