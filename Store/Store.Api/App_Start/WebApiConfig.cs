﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web.Http;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using Store.BusinessLogic.BL.Interfaces;
using Store.BusinessLogic.BL;
using SimpleInjector.Integration.WebApi;
using Store.Api.Providers;
using Microsoft.Owin.Security.OAuth;
using System.Web.Http.Cors;

namespace Store.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/plain"));
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/json"));
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);

            ConfigureDependencyInjection();

            //SwaggerConfig.Register();
        }

        public static void ConfigureDependencyInjection()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            container.Register<ICarrinhoBL, CarrinhoBL>(Lifestyle.Scoped);
            container.Register<ICategoriaBL, CategoriaBL>(Lifestyle.Scoped);
            container.Register<IClienteBL, ClienteBL>(Lifestyle.Scoped);
            container.Register<IProdutoBL, ProdutoBL>(Lifestyle.Scoped);
            container.Register<IContadoresBL, ContadoresBL>(Lifestyle.Scoped);

            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);
            container.Verify();
            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
        }
    }
}
