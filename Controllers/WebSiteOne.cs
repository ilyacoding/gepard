using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gepard.Core;
using Gepard.Core.HttpAction;
using Gepard.Core.Request;
using Gepard.Core.Response;

namespace Gepard.Controllers
{
    public class WebSiteOne : IRequestHandler
    {
        public IRequestHandler NextHandler { get; set; }

        public IHttpAction Handle(HttpRequest request)
        {
            if (request.Object.Host.Contains("s3.localhost"))
            {
                var httpHeaders = new HttpHeaders();
                return new Ok(httpHeaders, Encoding.UTF8.GetBytes("Nice website"));
            }
            return NextHandler != null ? NextHandler.Handle(request) : new NotImplemented();
        }
    }
}
