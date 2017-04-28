using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Gepard.Configuration.Server;
using Gepard.Controllers;

namespace Gepard.Core
{
    public class ControllersRegistry
    {
        private Dictionary<string, IController> Dictionary { get; set; }
        private IController DefaultController { get; set; }

        public ControllersRegistry()
        {
            Dictionary = new Dictionary<string, IController>();
        }

        public void Reg(string str, IController controller)
        {
            Dictionary.Add(str, controller);
        }

        public void RegDefault(IController controller)
        {
            DefaultController = controller;
        }

        public IController Get(string str)
        {
            str = str.Trim();
            if (!Dictionary.ContainsKey(str)) throw new Exception("501");
            return Dictionary[str];
        }
    }
}
