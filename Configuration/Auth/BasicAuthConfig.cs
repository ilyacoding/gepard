using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Gepard.Configuration.Auth
{
    [Serializable]
    public class BasicAuthConfig
    {
        [XmlElement("AuthName")]
        public string AuthName { get; set; }

        [XmlElement("Directory")]
        public string AuthDirectory { get; set; }

        [XmlElement("UserName")]
        public string UserName { get; set; }

        [XmlElement("Password")]
        public string Password { get; set; }
    }
}
