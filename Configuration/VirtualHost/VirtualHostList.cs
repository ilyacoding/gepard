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
        public List<VirtualHost> VirtualHosts { get; set; }
    }
}
