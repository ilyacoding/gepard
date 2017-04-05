using System;
using System.Xml.Serialization;

namespace Gepard.Configuration.Server
{
    [Serializable]
    [XmlRoot("ServerConfiguration")]
    public class ServerConfig
    {
        public string Ip { get; set; }
        public int Port { get; set; }
        public string DirectoryRoot { get; set; }
        public string ErrorLog { get; set; }
    }
}
