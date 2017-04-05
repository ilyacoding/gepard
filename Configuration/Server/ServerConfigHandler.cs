using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gepard.Configuration.Server
{
    public class ServerConfigHandler
    {
        public ServerConfigSerializer ServerConfigSerializer { get; set; }

        public ServerConfigHandler(ServerConfigSerializer serverConfigSerializer)
        {
            ServerConfigSerializer = serverConfigSerializer;
        }

        public ServerConfig LoadFromFile(string filePath)
        {
            var fileContent = File.ReadAllText(filePath);
            return ServerConfigSerializer.Deserialize(fileContent);
        }
    }
}
