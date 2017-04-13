using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gepard.Configuration.VirtualHost
{
    public class VirtualHostConfigHandler
    {
        public VirtualHostConfigSerializer VirtualHostConfigSerializer { get; set; }

        public VirtualHostConfigHandler(VirtualHostConfigSerializer virtualHostConfigSerializer)
        {
            VirtualHostConfigSerializer = virtualHostConfigSerializer;
        }

        public VirtualHostList LoadFromFile(string filePath)
        {
            var fileContent = File.ReadAllText(filePath);
            return VirtualHostConfigSerializer.Deserialize(fileContent);
        }

//        public void SaveToFile(VirtualHostList virtualHost)
//        {
//            var x = VirtualHostConfigSerializer.Serialize(virtualHost);
//            File.WriteAllText("v1.xml", x);
//        }
    }
}
