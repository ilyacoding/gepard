using System;
using System.Xml.Serialization;

namespace Gepard.Configuration.Auth
{
    [Serializable]
    public class BasicAuthConfig
    {
        [XmlElement("Directory")]
        public string AuthDirectory { get; set; }

        [XmlElement("Realm")]
        public string Realm { get; set; }

        [XmlElement("UserName")]
        public string UserName { get; set; }

        [XmlElement("Password")]
        public string Password { get; set; }
    }
}
