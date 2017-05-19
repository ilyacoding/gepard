using System.IO;
using System.Xml.Serialization;

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
