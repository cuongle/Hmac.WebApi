using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;

namespace WebApi.Core.Dependency
{
    public class WindsorDependencyScope : IDependencyScope
    {
        private readonly IDependencyScope _scope;
        private readonly Action<object> _releaseActions;
        private readonly List<object> _objectInstances;

        public WindsorDependencyScope(IDependencyScope scope, Action<object> releaseAction)
        {
            if (scope == null)
                throw new ArgumentNullException("scope");

            if (releaseAction == null)
                throw new ArgumentNullException("releaseAction");

            _scope = scope;
            _releaseActions = releaseAction;
            _objectInstances = new List<object>();
        }

        public object GetService(Type type)
        {
            object service = _scope.GetService(type);
            AddToScope(service);

            return service;
        }

        public IEnumerable<object> GetServices(Type type)
        {
            var services = _scope.GetServices(type);
            AddToScope(services);

            return services;
        }

        public void Dispose()
        {
            foreach (object instance in _objectInstances)
            {
                _releaseActions(instance);
            }

            _objectInstances.Clear();
        }

        private void AddToScope(params object[] services)
        {
            if (services.Any())
            {
                _objectInstances.AddRange(services);
            }
        }
    }
}
