using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gepard.Core.Response;

namespace Gepard.Core.HttpAction
{
    public class Ok : IHttpAction
    {
        public HttpResponse HttpResponse { get; set; }

        public Ok(byte[] dataBytes, string mime)
        {
            HttpResponse = new HttpResponse
            {
                HttpStatusCode = 200,
                IsSuccessStatus = true,
                Content = new HttpContent(){Data = dataBytes}
            };
            HttpResponse.Headers.Add("Content-Type", mime);
        }

    }
}
