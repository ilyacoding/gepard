using System;
using System.Collections.Generic;
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
        }
    }
}
