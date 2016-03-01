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
            Configuration conf = new Configuration("http://localhost:7777");

            HttpSelfHostServer server = new HttpSelfHostServer(conf.Config);
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
