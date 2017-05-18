using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gepard.Core.Response;

namespace Gepard.Core.HttpAction
{
    public class PartialContent : IHttpAction
    {
        public HttpResponse HttpResponse { get; set; }

        public PartialContent(HttpHeaders httpHeaders, byte[] dataBytes)
        {
            HttpResponse = new HttpResponse
            {
                HttpStatusCode = 206,
                IsSuccessStatus = true,
                Content = new HttpContent() { Data = dataBytes },
                Headers = httpHeaders
            };
        }
    }
}
