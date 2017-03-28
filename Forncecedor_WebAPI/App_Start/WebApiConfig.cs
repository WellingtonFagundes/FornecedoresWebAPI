using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace Forncecedor_WebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            //Removendo retorno do formato XML
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            config.Formatters.JsonFormatter.MediaTypeMappings.Add(new QueryStringMapping("tipo", "json", "application/json"));

            config.Routes.MapHttpRoute(
                name: "ServicoWebAPI",
                routeTemplate: "ServicoWebAPI/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
