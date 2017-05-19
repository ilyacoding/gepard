using System;
using System.IO;
using System.Text;
using Gepard.Configuration.VirtualHost;
using Gepard.Core.HttpAction;
using Gepard.Core.HttpFields;
using Gepard.Core.HttpHelpers;
using Gepard.Core.Logs;
using Gepard.Core.Requests;
using Gepard.Core.Responses;

namespace Gepard.Core.Main
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
            HttpRequest requestObject = null;
            HttpResponse httpResponse = null;

            try
            {
                requestObject = new HttpRequest(str);
            }
            catch
            {
                httpResponse = new NotFound().HttpResponse;
            }

            var virtualHost = VirtualHostList.GetVirtualHost(requestObject != null ? requestObject.Host : "");
            var request = new Request(requestObject, virtualHost);

            var accessLogHandler = new AccessLogHandler(Path.Combine(DirectoryRoot, "logs", virtualHost.AccessLog), clientIp, requestObject != null ? requestObject.UserAgent : "");

            if (httpResponse == null)
            {
                httpResponse = ChainControllerHandler.Execute(request).HttpResponse;
            }

            accessLogHandler.WriteInfo(request.Object.Method + " /" + request.Object.Uri.Url + " " + HttpResponseStatus.Get(httpResponse.HttpStatusCode));

            httpResponse.Headers.Add("Server", HttpServer.HttpServerName);
            httpResponse.Headers.Add("Date", new HttpDate(DateTime.Now).ToString());

            if (httpResponse.Content.IncludeBody && httpResponse.Content.Data == null)
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
