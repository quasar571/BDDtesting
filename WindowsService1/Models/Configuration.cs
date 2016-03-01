using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.SelfHost;
using Ninject;

namespace WindowsService1
{
    public class Configuration
    {
        private string url;

        public HttpSelfHostConfiguration Config
        {
            get
            {
                var config = new HttpSelfHostConfiguration(url);
                config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
                config.Routes.MapHttpRoute(
                    name: "Api",
                    routeTemplate: "{controller}",
                    defaults: new { controller = "Time" }
                    );

                var kernel = new StandardKernel();
                kernel.Load(Assembly.GetExecutingAssembly());
                config.DependencyResolver = new NinjectDependencyResolver(kernel);

                return config;
            }
        }

        public Configuration(string url)
        {
            this.url = url;
        }
    }
}
