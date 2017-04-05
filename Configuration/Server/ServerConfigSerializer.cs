using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Gepard.Configuration.Server
{
    public class ServerConfigSerializer
    {
        public string Serialize(ServerConfig serverConfig)
        {
            var formatter = new XmlSerializer(typeof(ServerConfig));
            var stream = new StringWriter();
            var ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            formatter.Serialize(stream, serverConfig, ns);
            return stream.ToString();
        }

        public ServerConfig Deserialize(string str)
        {
            var formatter = new XmlSerializer(typeof(ServerConfig));
            var stream = new StringReader(str);
            return (ServerConfig)formatter.Deserialize(stream);
        }
    }
}
