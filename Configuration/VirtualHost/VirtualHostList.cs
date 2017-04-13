using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Gepard.Configuration.VirtualHost
{
    [Serializable]
    [XmlRoot("VirtualHostsConfiguration")]
    public class VirtualHostList
    {
        private List<VirtualHost> VirtualHosts { get; }

        public bool HasVirtualHostHost(string host)
        {
            return VirtualHosts.Any(element => element.ServerName == host || element.ServerAlias == host);
        }

        public VirtualHost GetVirtualHost(string host)
        {
            return VirtualHosts.Find(virtualhost => virtualhost.ServerName == host || virtualhost.ServerAlias == host);
        }
    }
}
