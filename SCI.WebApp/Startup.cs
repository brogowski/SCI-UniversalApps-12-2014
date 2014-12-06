using System.Net.Http.Formatting;
using System.Web.Http;
using Owin;
using SCI.BL.Factories;
using SCI.WebApp.Formatters;

namespace SCI.WebApp
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Formatters.Remove(config.Formatters.XmlFormatter);
            SetupJsonFormatter(config);

            appBuilder.UseWebApi(config);
        }

        private static void SetupJsonFormatter(HttpConfiguration config)
        {
            foreach (var formatter in config.Formatters)
            {
                var jsonFormatter = formatter as JsonMediaTypeFormatter;
                if (jsonFormatter == null)
                    continue;

                jsonFormatter.SerializerSettings.Converters.Add(new WallEntryJsonConverter(new WallEntryFactory()));
            }
        }
    } 
}