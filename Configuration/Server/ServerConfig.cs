using System;
using System.Xml.Serialization;

namespace Gepard.Configuration.Server
{
    [Serializable]
    [XmlRoot("ServerConfiguration")]
    public class ServerConfig
    {
        [XmlElement("Ip")]
        public string Ip { get; set; }

        [XmlElement("Port")]
        public int Port { get; set; }

        [XmlElement("ServerName")]
        public string ServerName { get; set; }

        [XmlElement("KeepAliveTimeout")]
        public int KeepAliveTimeout { get; set; }

        [XmlElement("DirectoryRoot")]
        public string DirectoryRoot { get; set; }

        [XmlElement("ErrorLog")]
        public string ErrorLog { get; set; }
    }
}
