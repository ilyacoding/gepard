using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gepard.Core.Response;

namespace Gepard.Core.HttpAction
{
    public class NotSatisfiable : IHttpAction
    {
        public HttpResponse HttpResponse { get; set; }

        public NotSatisfiable()
        {
            HttpResponse = new HttpResponse
            {
                HttpStatusCode = 416,
                IsSuccessStatus = false
            };
        }
    }
}
