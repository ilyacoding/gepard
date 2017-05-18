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

        public NotModified(HttpHeaders httpHeaders)
        {
            HttpResponse = new HttpResponse()
            {
                HttpStatusCode = 304,
                IsSuccessStatus = true,
                Content = new HttpContent() { Data = null },
                Headers = httpHeaders
            };
        }
    }
}
