using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using Gepard.Configuration.Server;
using Gepard.Configuration.VirtualHost;

namespace Gepard
{
    partial class GepardService : ServiceBase
    {
        public Application Application { get; set; }

        public GepardService()
        {
            InitializeComponent();
            Application = new Application("config", new ServerConfigSerializer(), new VirtualHostConfigSerializer());
        }

        protected override void OnStart(string[] args)
        {
            Application.Start();
        }

        protected override void OnStop()
        {
            Application.Stop();
        }
    }
}
