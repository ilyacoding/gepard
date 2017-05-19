using System;
using System.Xml.Serialization;

namespace Gepard.Configuration.VirtualHost
{
    [Serializable]
    [XmlRoot("FileName")]
    public class IndexFile
    {
        [XmlElement("FileName")]
        public string FileName { get; set; }
    }
}
