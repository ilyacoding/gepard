using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Gepard.Configuration.Server;
using Gepard.Configuration.VirtualHost;

namespace Gepard
{
    public class HttpProcessor
    {
        private ServerConfig ServerConfig { get; }

        public HttpProcessor(ServerConfig serverConfig, VirtualHostList virtualHostList)
        {
            ServerConfig = serverConfig;
        }

      
        public void PushTo(NetworkStream stream, Response response)
        {
            var data = $"{Gepard.HttpServer.HttpVersion} {response.Status}\r\nContent-type: {response.Mime}\r\nAccept-Ranges: bytes\r\nContent-Length: {response.Data.Length}\r\n\r\n";
            var dataBytes = Encoding.UTF8.GetBytes(data);
            
            stream.Write(dataBytes, 0, dataBytes.Length);
            stream.Write(response.Data, 0, response.Data.Length);
        }

        public string GetMime(string extension)
        {
            throw new NotImplementedException();
        }

        
    }
}
