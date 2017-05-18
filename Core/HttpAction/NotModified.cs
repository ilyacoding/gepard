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
        public HttpResponse HttpResponse { get; set; }

        public NotModified(HttpHeaders httpHeaders, bool includeBody = true)
        {
            HttpResponse = new HttpResponse()
            {
                HttpStatusCode = 304,
                IsSuccessStatus = true,
                Content = new HttpContent() { Data = null, IncludeBody = includeBody },
                Headers = httpHeaders
            };
        }
    }
}
