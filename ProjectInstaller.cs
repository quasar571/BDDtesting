using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;
using System.Threading.Tasks;

namespace WindowsService1
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();
        }

        protected override void OnAfterInstall(System.Collections.IDictionary savedState)
        {
            base.OnAfterInstall(savedState);
            using (var serviceController = new ServiceController(this.serviceInstaller1.ServiceName, Environment.MachineName))
                serviceController.Start();
        }
    }
}
