using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gepard.Core.HttpFields;
using Gepard.Core.Main;
using Gepard.Core.Responses;

namespace Gepard.Core.HttpAction
{
    public class Created : IHttpAction
    {
        public HttpResponse HttpResponse { get; set; }

        public Created(HttpHeaders httpHeaders, byte[] dataBytes, bool includeBody = true)
        {
            HttpResponse = new HttpResponse
            {
                HttpStatusCode = 201,
                IsSuccessStatus = true,
                Content = new HttpContent() { Data = dataBytes, IncludeBody = includeBody },
                Headers = httpHeaders
            };
        }
    }
}
