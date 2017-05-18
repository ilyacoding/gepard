using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Gepard.Configuration.Auth;

namespace Gepard.Configuration.VirtualHost
{
    [Serializable]
    [XmlRoot("VirtualHost")]
    public class VirtualHost
    {
        [XmlElement("ServerName")]
        public string ServerName { get; set; }

        [XmlElement("ServerAlias")]
        public string ServerAlias { get; set; }

        [XmlElement("Directory")]
        public string Directory { get; set; }

        [XmlElement("ErrorLog")]
        public string ErrorLog { get; set; }

        [XmlElement("AccessLog")]
        public string AccessLog { get; set; }

        [XmlArray("DefaultIndex")]
        public List<IndexFile> DefaultIndex { get; set; }

        [XmlArray("BasicAuthentication")]
        public List<BasicAuthConfig> BasicAuthConfigs { get; set; }

        [XmlArray("DigestAuthentication")]
        public List<DigestAuthConfig> DigestAuthConfigs { get; set; }
    }
}
