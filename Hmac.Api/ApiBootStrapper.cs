using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Hmac.Application;
using Hmac.Data;
using Newtonsoft.Json;

namespace Hmac.Api
{
    public class ApiBootStrapper
    {
        private void RegisterAutofac(HttpConfiguration configuration)
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule(new ApplicationModule());
            builder.RegisterModule(new DataModule());

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly())
                    .InstancePerRequest();

            var container = builder.Build();

            var autofacResolver = new AutofacWebApiDependencyResolver(container);
            configuration.DependencyResolver = autofacResolver;
        }

        private void RegisterApiRoutes(HttpConfiguration configuration)
        {
            configuration.Routes.MapHttpRoute(
              name: "CustomizedApi",
              routeTemplate: "api/{controller}/{action}/{id}",
              defaults: new { id = RouteParameter.Optional }
          );

            configuration.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }

        private void RegisterFilters(HttpConfiguration configuration)
        {
        }

        private void ConfigureFormatter(HttpConfiguration configuration)
        {
            var formatters = configuration.Formatters;
            formatters.Remove(formatters.XmlFormatter);

            var jsonFormatter = configuration.Formatters.JsonFormatter;
            jsonFormatter.SerializerSettings = new JsonSerializerSettings()
            {
                DateTimeZoneHandling = DateTimeZoneHandling.Utc
            };
        }

        public void Boot(HttpConfiguration configuration)
        {
            RegisterApiRoutes(configuration);
            RegisterFilters(configuration);
            RegisterAutofac(configuration);
            ConfigureFormatter(configuration);
        }
    }
}

