using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gepard.Core.Response;

namespace Gepard.Core.HttpAction
{
    public class NotModified : IHttpAction
    {
        public int Code { get; set; }
        public object Object { get; set; }

        public NotModified()
        {
            Code = 204;
            Object = null;
        }

        public HttpResponse HttpResponse { get; set; }
    }
}
