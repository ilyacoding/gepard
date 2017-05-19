using System.IO;

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
    }
}
