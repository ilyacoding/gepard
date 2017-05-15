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
    //public class HttpProcessor
    //{
    //    private const string HTTP_SERVER = "Gepard 0.1";
    //    private ServerConfig ServerConfig { get; }
    //
    //    public HttpProcessor(ServerConfig serverConfig, VirtualHostList virtualHostList)
    //    {
    //        ServerConfig = serverConfig;
    //    }
    //
    //    public Response GetResponse(Request request, VirtualHost virtualHost)
    //    {
    //        switch (request.Method)
    //        {
    //            case "GET":
    //                return GetProcess(request, virtualHost);
    //
    //            default:
    //                return MakeErrorFromFile(501);
    //        }
    //    }
    //
    //    public Response GetErrorResponse(int errorCode)
    //    {
    //        return MakeErrorFromFile(errorCode);
    //    }
    //
    //    private Response GetProcess(Request request, VirtualHost virtualHost)
    //    {
    //        var file = Path.Combine(ServerConfig.DirectoryRoot, "www", virtualHost.Directory, request.Uri.Url);
    //        if (File.Exists(file))
    //        {
    //            return MakeFromFile(new FileInfo(file));
    //        }
    //        else if (Directory.Exists(file))
    //        {
    //            var directoryInfo = new DirectoryInfo(file);
    //            var fileList = directoryInfo.GetFiles();
    //            foreach (var f in fileList)
    //            {
    //                var fileName = f.Name;
    //                if (fileName.Contains("index.html") || fileName.Contains("index"))
    //                {
    //                    return MakeFromFile(f);
    //                }
    //            }
    //            return MakeErrorFromFile(404);
    //        }
    //        else
    //        {
    //            return MakeErrorFromFile(404);
    //        }
    //    }
    //
    //    public Response MakeFromFile(FileInfo fileInfo)
    //    {
    //        var filePath = fileInfo.FullName;
    //        var data = File.ReadAllBytes(filePath);
    //        return new Response(ResponseStatus.Get(200), data, MimeType.GetByExtension(fileInfo.Extension));
    //    }
    //
    //    public Response MakeErrorFromFile(int errorCode)
    //    {
    //        var data = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "pages", "error.html"));
    //        data = data.Replace("{CODE}", errorCode.ToString());
    //        data = data.Replace("{CODE-DESCRIPTION}", ResponseStatus.Get(errorCode));
    //        data = data.Replace("{SERVER}", HTTP_SERVER + " / " + Environment.OSVersion);
    //        return new Response(ResponseStatus.Get(errorCode), Encoding.UTF8.GetBytes(data), "text");
    //    }
    //
    //    public void PushTo(NetworkStream stream, Response response)
    //    {
    //        var data = $"{Gepard.HttpServer.HttpVersion} {response.Status}\r\nContent-type: {response.Mime}\r\nAccept-Ranges: bytes\r\nContent-Length: {response.Data.Length}\r\n\r\n";
    //        var dataBytes = Encoding.UTF8.GetBytes(data);
    //        
    //        stream.Write(dataBytes, 0, dataBytes.Length);
    //        stream.Write(response.Data, 0, response.Data.Length);
    //    }
    //
    //    public string GetMime(string extension)
    //    {
    //        throw new NotImplementedException();
    //    }
    //
    //    
    //}
}
