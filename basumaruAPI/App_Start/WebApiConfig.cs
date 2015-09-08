using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Net.Http.Formatting;

namespace basumaruAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API の設定およびサービス
            config.Formatters.XmlFormatter.AddQueryStringMapping("format", "xml", "application/xml");
            config.Formatters.JsonFormatter.AddQueryStringMapping("format", "json", "application/json");

            config.EnableCors();

            // Web API ルート
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
