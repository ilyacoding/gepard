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
        private ServerConfig ServerConfig { get; set; }
        private VirtualHostList VirtualHostList { get; set; }

        public HttpProcessor(ServerConfig serverConfig, VirtualHostList virtualHostList)
        {
            ServerConfig = serverConfig;
            VirtualHostList = virtualHostList;
        }

        public Response Get(Request request)
        {
            switch (request.Method)
            {
                case "GET":
                    return GetProcess(request);

                default:
                    return MakeErrorFromFile(501);
            }
        }
        
        private Response GetProcess(Request request)
        {
            var file = ServerConfig.DirectoryRoot + request.Uri;

            if (File.Exists(file))
            {
                return MakeFromFile(file);
            }
            else if (Directory.Exists(file))
            {
                var directoryInfo = new DirectoryInfo(file);
                var fileList = directoryInfo.GetFiles();
                foreach (var f in fileList)
                {
                    var fileName = f.Name;
                    if (fileName.Contains("index.html"))
                    {
                        return MakeFromFile(f.FullName);
                    }
                }
                return MakeErrorFromFile(404);
            }
            else
            {
                return MakeErrorFromFile(404);
            }
        }

        public Response MakeFromFile(string filePath)
        {
            var data = File.ReadAllBytes(filePath);
            return new Response(ResponseStatus.Get(200), data, "image");
        }

        public Response MakeErrorFromFile(int errorCode)
        {
            var data = File.ReadAllBytes(Path.Combine(Environment.CurrentDirectory, "pages", "error.html"));
            return new Response(ResponseStatus.Get(200), data, "text");
            //                        var data = File.ReadAllBytes(filePath);
            //                        return new Response(ResponseStatus.Get(200), data, "html/text");
        }

        public void Post(NetworkStream stream, Response response)
        {
            var data = $"{HttpServer.HttpVersion} {response.Status}\r\nContent-type: {response.Mime}\r\nAccept-Ranges: bytes\r\nContent-Length: {response.Data.Length}\r\n\r\n";
            var dataBytes = Encoding.UTF8.GetBytes(data);

            stream.Write(dataBytes, 0, dataBytes.Length);
            stream.Write(response.Data, 0, response.Data.Length);
            //
            //            var dataBytes = Encoding.UTF8.GetBytes(data);

            //            var streamWriter = new StreamWriter(stream);
            //            streamWriter.WriteLine(string.Format("{0} {1}\r\nContent-type: {2}\r\nAccept-Ranges: bytes\r\nContent-Length: {3}\r\n", TcpServer.HttpVersion, Status, Mime, 20));

            stream.Write(dataBytes, 0, dataBytes.Length);
        }

        public string GetMime(string extension)
        {
            throw new NotImplementedException();
        }


    }
}
