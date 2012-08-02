using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;
using Castle.Windsor;

namespace WebApi.Core.Dependency
{
    public class WindsorDependencyResolver : IDependencyResolver
    {
        private readonly IWindsorContainer _windsorContainer;

        public WindsorDependencyResolver(IWindsorContainer windsorContainer)
        {
            if (windsorContainer == null)
                throw new ArgumentNullException("windsorContainer");

            _windsorContainer = windsorContainer;
        }

        public object GetService(Type serviceType)
        {
            if (!_windsorContainer.Kernel.HasComponent(serviceType))
                return null;

            return _windsorContainer.Resolve(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            if (!_windsorContainer.Kernel.HasComponent(serviceType))
                return Enumerable.Empty<object>();

            return _windsorContainer.ResolveAll(serviceType).Cast<object>();
        }

        public IDependencyScope BeginScope()
        {
            return new WindsorDependencyScope(this, _windsorContainer.Release);
        }

        public void Dispose()
        {
        }
    }
}
