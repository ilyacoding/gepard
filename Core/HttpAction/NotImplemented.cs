using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gepard.Core.Response;

namespace Gepard.Core.HttpAction
{
    public class NotImplemented : IHttpAction
    {
        public HttpResponse HttpResponse { get; set; }

        public NotImplemented(bool includeBody = true)
        {
            HttpResponse = new HttpResponse
            {
                HttpStatusCode = 501,
                IsSuccessStatus = false,
                Content = new HttpContent() { Data = null, IncludeBody = includeBody },
            };
        }

    }
}
