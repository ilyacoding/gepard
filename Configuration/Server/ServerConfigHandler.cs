using System.IO;

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
