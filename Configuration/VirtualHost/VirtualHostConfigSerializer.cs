using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Gepard.Configuration.Server;

namespace Gepard.Configuration.VirtualHost
{
    public class VirtualHostConfigSerializer
    {
        public string Serialize(VirtualHostList virtualHostList)
        {
            var formatter = new XmlSerializer(typeof(VirtualHostList));
            var stream = new StringWriter();
            var ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            formatter.Serialize(stream, virtualHostList, ns);
            return stream.ToString();
        }

        public VirtualHostList Deserialize(string str)
        {
            var formatter = new XmlSerializer(typeof(VirtualHostList));
            var stream = new StringReader(str);
            return (VirtualHostList)formatter.Deserialize(stream);
        }
    }
}
