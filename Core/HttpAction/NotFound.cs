using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gepard.Core.Response;

namespace Gepard.Core.HttpAction
{
    public class NotFound : IHttpAction
    {
        public HttpResponse HttpResponse { get; set; }

        public NotFound()
        {
            HttpResponse = new HttpResponse
            {
                HttpStatusCode = 404,
                IsSuccessStatus = false
            };
        }

        public NotFound(byte[] dataBytes, string mime)
        {
            HttpResponse = new HttpResponse
            {
                HttpStatusCode = 404,
                IsSuccessStatus = false,
                Content = new HttpContent() { Data = dataBytes }
            };
            HttpResponse.Headers.Add("Content-Type", mime);
        }
    }
}
