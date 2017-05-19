using Gepard.Core;
using Gepard.Core.HttpAction;
using Gepard.Core.HttpFields;
using Gepard.Core.Main;
using Gepard.Core.Requests;

namespace Gepard.Controllers
{
    public class OptionsMethodHandler : IRequestHandler
    {
        public IRequestHandler NextHandler { get; set; }

        public IHttpAction Handle(Request request)
        {
            if (request.Object.Method == "OPTIONS")
            {
                var httpHeaders = new HttpHeaders();

                httpHeaders.Add("Allow", "OPTIONS, GET, HEAD, POST");

                return new Ok(httpHeaders, null, false);
            }
            return NextHandler != null ? NextHandler.Handle(request) : new NotImplemented();
        }
    }
}
