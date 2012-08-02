using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace WebApi.Core.Dependency
{
    public class WindsorHttpControllerSelector : DefaultHttpControllerSelector
    {
        private readonly HttpConfiguration _configuration;
        private readonly Assembly _controllerAssembly;

        public WindsorHttpControllerSelector(
            HttpConfiguration configuration, Assembly controllerAssembly)
            : base(configuration)
        {
            if (configuration == null)
                throw new ArgumentNullException("configuration");

            if (controllerAssembly == null)
                throw new ArgumentNullException("controllerAssembly");

            _configuration = configuration;
            _controllerAssembly = controllerAssembly;
        }

        public override HttpControllerDescriptor SelectController(HttpRequestMessage request)
        {
            var controllerName = base.GetControllerName(request);
            var controllerType = GetControllerType(controllerName);

            return new HttpControllerDescriptor(
                _configuration, controllerName, controllerType);
        }

        private IEnumerable<Type> GetAllControllerTypes()
        {
            var controllerTypes = _controllerAssembly.GetTypes().AsEnumerable()
                .Where(type => type.IsClass && !type.IsAbstract && type.IsSubclassOf(typeof(ApiController)));

            return controllerTypes;
        }

        private Type GetControllerType(string controllerName)
        {
            string controllerFullName = string.Format("{0}Controller", controllerName);
            var controllerTypes = GetAllControllerTypes();

            Type controllerType = controllerTypes
                .SingleOrDefault(type => type.Name.Equals(controllerFullName, StringComparison.OrdinalIgnoreCase));

            return controllerType;
        }
    }
}
