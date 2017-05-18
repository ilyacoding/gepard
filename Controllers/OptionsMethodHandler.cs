using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gepard.Core;
using Gepard.Core.FileHandling;
using Gepard.Core.HttpAction;
using Gepard.Core.Request;
using Gepard.Core.Response;

namespace Gepard.Controllers
{
    public class OptionsMethodHandler : IRequestHandler
    {
        public IRequestHandler NextHandler { get; set; }

        public IHttpAction Handle(HttpRequest request)
        {
            if (request.Object.Method == "OPTIONS")
            {
                var httpHeaders = new HttpHeaders();

                httpHeaders.Add("Allow", "OPTIONS, GET, HEAD");

                return new Ok(httpHeaders, null, false);
            }
            return NextHandler != null ? NextHandler.Handle(request) : new NotImplemented();
        }
    }
}
