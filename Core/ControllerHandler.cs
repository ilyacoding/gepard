using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Gepard.Configuration.Server;
using Gepard.Configuration.VirtualHost;
using Gepard.Core.Logs;
using Gepard.Core.Request;
using Gepard.Core.Response;

namespace Gepard.Core
{
    public class ControllerHandler
    {
        private ChainControllerHandler ChainControllerHandler { get; set; }
        private VirtualHostList VirtualHostList { get; set; }

        private string ServerName { get; set; }
        private string DirectoryRoot { get; set; }

        public ControllerHandler(VirtualHostList virtualHostList, ChainControllerHandler chainControllerHandler, string serverName, string directoryRoot)
        {
            VirtualHostList = virtualHostList;
            ChainControllerHandler = chainControllerHandler;
            ServerName = serverName;
            DirectoryRoot = directoryRoot;
        }

        public ByteResponse Execute(string str, string clientIp)
        {
            var requestObject = new Request.Request(str);
            var virtualHost = VirtualHostList.GetVirtualHost(requestObject.Host);

            var accessLogHandler = new AccessLogHandler(Path.Combine(DirectoryRoot, "logs", virtualHost.AccessLog), clientIp, requestObject.UserAgent);
            
            var request = new HttpRequest(requestObject, virtualHost);
            
            var httpResponse = ChainControllerHandler.Execute(request).HttpResponse;

            accessLogHandler.WriteInfo(request.Object.Method + " /" + request.Object.Uri.Url + " " + HttpResponseStatus.Get(httpResponse.HttpStatusCode));

            httpResponse.Headers.Add("Server", HttpServer.HttpServerName);
            httpResponse.Headers.Add("Date", new HttpDate(DateTime.Now).ToString());

            if (!httpResponse.IsSuccessStatus)
            {
                httpResponse.Content.Data = GetErrorBody(httpResponse.HttpStatusCode);
            }

            return new ByteResponse(httpResponse.GetBytes(), request.Object.KeepAlive);
        }

        private byte[] GetErrorBody(int errorCode)
        {
            var data = File.ReadAllText(Path.Combine(DirectoryRoot, "pages", "error.html"));
            data = data.Replace("{CODE}", errorCode.ToString());
            data = data.Replace("{CODE-DESCRIPTION}", HttpResponseStatus.Get(errorCode));
            data = data.Replace("{SERVER}", ServerName + " / " + Environment.OSVersion);
            return Encoding.UTF8.GetBytes(data);
        }
    }
}
