using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gepard.Core.Response;

namespace Gepard.Core.HttpAction
{
    public class Unauthorized : IHttpAction
    {
        public HttpResponse HttpResponse { get; set; }

        public Unauthorized(HttpHeaders httpHeaders, byte[] dataBytes)
        {
            HttpResponse = new HttpResponse
            {
                HttpStatusCode = 401,
                IsSuccessStatus = false,
                Content = new HttpContent() { Data = dataBytes },
                Headers = httpHeaders
            };
        }
    }
}
