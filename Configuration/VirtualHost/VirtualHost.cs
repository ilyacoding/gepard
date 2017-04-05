using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Gepard.Configuration.VirtualHost
{
    [Serializable]
    [XmlRoot("VirtualHost")]
    public class VirtualHost
    {
        public string ServerName { get; set; }
        public string ServerAlias { get; set; }
        public string Directory { get; set; }
        public string ErrorLog { get; set; }
        public string CustomLog { get; set; }
    }
}
