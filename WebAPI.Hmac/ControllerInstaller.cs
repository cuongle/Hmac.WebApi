using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using WebAPI.Hmac.Controllers;
using WebApi.Core;

namespace WebAPI.Hmac
{
    public class ControllerInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container
                .Register(Component.For<ValuesController>().LifestylePerWebRequest())
                .Register(Component.For<IAccountRepository>().ImplementedBy<AccountRepository>())
                .Register(Component.For<IValueService>().ImplementedBy<ValueService>());
        }
    }
}