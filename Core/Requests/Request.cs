using Gepard.Configuration.VirtualHost;

namespace Gepard.Core.Requests
{
    public class Request
    {
        public HttpRequest Object { get; set; }
        public VirtualHost VirtualHost { get; set; }

        public Request(HttpRequest requestObject, VirtualHost virtualHost)
        {
            Object = requestObject;
            VirtualHost = virtualHost;
        }
    }
}
