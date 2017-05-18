using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gepard.Configuration.VirtualHost;
using Gepard.Core.Logs;

namespace Gepard.Core.Request
{
    public class HttpRequest
    {
        public Request Object { get; set; }
        public VirtualHost VirtualHost { get; set; }

        public HttpRequest(Request requestObject, VirtualHost virtualHost)
        {
            Object = requestObject;
            VirtualHost = virtualHost;
        }
    }
}
