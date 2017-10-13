using CrudExamples.WebApi.Models;
using CrudExamples.WebApi.Models.Database;
using CrudExamples.WebApi.Models.Services;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Http;

namespace CrudExamples.WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            var container = new UnityContainer();
            container.RegisterType<IVesselsService, VesselsService>(new TransientLifetimeManager());
            container.RegisterType<VesselsContext>(new TransientLifetimeManager(), new InjectionConstructor("Default"));

            config.DependencyResolver = new UnityResolver(container);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
