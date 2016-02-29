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
using Ninject.Web.WebApi;
using System.Web.Http.Validation;
using Ninject.Web.WebApi.Filter;
using Ninject.Extensions.Wcf;

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
            //kernel.Bind<DefaultModelValidatorProviders>().ToConstant(new DefaultModelValidatorProviders(config.Services.GetServices(typeof(ModelValidatorProvider)).Cast<ModelValidatorProvider>()));
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
