using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gepard.Core.Response;

namespace Gepard.Core.HttpAction
{
    public class BadRequest : IHttpAction
    {
        public int Code { get; set; }
        public object Object { get; set; }

        public BadRequest()
        {
            Code = 400;
            Object = null;
        }

        public HttpResponse HttpResponse { get; set; }
    }
}
