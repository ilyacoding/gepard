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
    public class PartGetMethodHandler : IRequestHandler
    {
        public IRequestHandler NextHandler { get; set; }

        public IHttpAction Handle(HttpRequest request)
        {
            if (request.Object.Method == "GET" && 1 == -1)
            {
                return new NotFound();
            }
            return NextHandler != null ? NextHandler.Handle(request) : new NotImplemented();
        }
    }
}
