using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Gepard.Configuration.Server;

namespace Gepard
{
    public class Response
    {
        public string Status { get; set; }
        public string Data { get; set; }
        public string Mime { get; set; }

        public Response(string status, string data, string mime)
        {
            Status = status;
            Data = data;
            Mime = mime;
        }

        public void Post(NetworkStream stream)
        {
            var data = $"{TcpServer.HttpVersion} {Status}\r\nContent-type: {Mime}\r\nContent-Length: {Data.Length}\r\n\r\n{Data}";

            var dataBytes = Encoding.UTF8.GetBytes(data);

//            var streamWriter = new StreamWriter(stream);
//            streamWriter.WriteLine(string.Format("{0} {1}\r\nContent-type: {2}\r\nAccept-Ranges: bytes\r\nContent-Length: {3}\r\n", TcpServer.HttpVersion, Status, Mime, 20));

            stream.Write(dataBytes, 0, dataBytes.Length);
        }

        public static Response From(Request request, ServerConfig serverConfig)
        {
            if (request.Method.Equals("GET"))
            {
                var file = serverConfig.DirectoryRoot + request.Uri;

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
                            return MakeFromFile(fileName);
                        }
                    }
                    return MakeErrorFromFile(404);
                }
                else
                {
                    return MakeErrorFromFile(404);
                }
            }
            return MakeErrorFromFile(405);
        }

        public static Response MakeFromFile(string filePath)
        {
            var data = File.ReadAllText(filePath);
            return new Response(ResponseStatus.Get(200), data, "text");
        }

        public static Response MakeErrorFromFile(int errorCode)
        {
            var data = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "pages", "error.html"));
            return new Response(ResponseStatus.Get(200), data, "text");
            //            var data = File.ReadAllBytes(filePath);
            //            return new Response(ResponseStatus.Get(200), data, "html/text");
        }
    }
}
