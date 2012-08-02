using System.Web.Http;
using System.Web.Http.Dispatcher;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using System.Reflection;

namespace WebApi.Core.Dependency
{
    public class CustomizedWebApiInstaller: IWindsorInstaller
    {
        private readonly Assembly _controllerAssembly;
        private readonly HttpConfiguration _globalConfiguration;

        public CustomizedWebApiInstaller(HttpConfiguration globalConfiguration, Assembly controllerAssembly)
        {
            _globalConfiguration = globalConfiguration;
            _controllerAssembly = controllerAssembly;
        }

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container
                .Register(Component.For<IHttpControllerSelector>()
                    .ImplementedBy<WindsorHttpControllerSelector>()
                    .DependsOn( new { controllerAssembly = _controllerAssembly }))

                .Register(Component.For<HttpConfiguration>().Instance(_globalConfiguration));
        }
    }
}
