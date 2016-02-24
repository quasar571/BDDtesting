using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.SelfHost;
using Ninject;
using System.Reflection;

namespace WindowsService1
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            var config = new HttpSelfHostConfiguration("http://localhost:7777");
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            config.Routes.MapHttpRoute(
                name: "Api",
                routeTemplate: "",
                defaults: new { controller = "Time"}
                );

            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            config.DependencyResolver = new NinjectDependencyResolver(kernel);

            HttpSelfHostServer server = new HttpSelfHostServer(config);
            server.OpenAsync().Wait();
        }

        public void OnDebug()
        {
            OnStart(null);
        }

        protected override void OnStop()
        {
        }
    }

    
}
