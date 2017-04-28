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
        [XmlArray("VirtualHosts")]
        public List<VirtualHost> VirtualHosts { get; set; }

        [XmlElement("DefaultVirtualHost")]
        public VirtualHost DefaultVirtualHost { get; set; }

        public VirtualHostList()
        {
            VirtualHosts = new List<VirtualHost>();
            DefaultVirtualHost = new VirtualHost();
        }

//        public bool HasVirtualHost(string host)
//        {
//            return VirtualHosts.Any(element => element.ServerName == host || element.ServerAlias == host);
//        }

        public VirtualHost GetVirtualHost(string host)
        {
            foreach (var virtualHost in VirtualHosts)
            {
                if (virtualHost.ServerName == host || virtualHost.ServerAlias == host)
                {
                    return virtualHost;
                }
            }
            return DefaultVirtualHost;
//            return VirtualHosts.Find(virtualhost => virtualhost.ServerName == host || virtualhost.ServerAlias == host);
        }
    }
}
