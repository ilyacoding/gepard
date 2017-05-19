using System.Text;
using Gepard.Core;
using Gepard.Core.HttpAction;
using Gepard.Core.HttpFields;
using Gepard.Core.Main;
using Gepard.Core.Requests;

namespace Gepard.Controllers
{
    public class WebSiteOne : IRequestHandler
    {
        public IRequestHandler NextHandler { get; set; }

        public IHttpAction Handle(Request request)
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
