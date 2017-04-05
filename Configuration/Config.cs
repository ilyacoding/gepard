using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Gepard.Configuration.Server;

namespace Gepard.Configuration
{
    [Serializable]
    [XmlRoot("Configuration")]
    public class Config
    {
        [XmlElement("Server")]
        public ServerConfig ServerConfig { get; set; }
        [XmlArray("VirtualHosts")]
        public List<VirtualHost.VirtualHost> VirtualHosts { get; set; }

        public Config()
        {
            VirtualHosts = new List<VirtualHost.VirtualHost>();
            ServerConfig = new ServerConfig();
        }
    }
}
