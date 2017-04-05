using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Gepard.Configuration
{
    public class ConfigSerializer
    {
        public void Serialize(Config virtualHostConfig, string filePath)
        {
            var formatter = new XmlSerializer(typeof(Config));
            var ns = new XmlSerializerNamespaces();

            ns.Add("", "");
            File.Delete(filePath);
            using (var fs = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, virtualHostConfig, ns);
            }
        }

        public Config Deserialize(string filePath)
        {
            var formatter = new XmlSerializer(typeof(Config));
            using (var fs = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                return (Config)formatter.Deserialize(fs);
            }
        }
    }
}
