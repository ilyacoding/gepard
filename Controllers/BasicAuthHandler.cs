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
    public class BasicAuthHandler : IRequestHandler
    {
        public IRequestHandler NextHandler { get; set; }

        public IHttpAction Handle(HttpRequest request)
        {
            if (request.VirtualHost.AuthConfigs != null)
            {
                foreach (var authConfig in request.VirtualHost.AuthConfigs)
                {
                    if (request.Object.Uri.Url.StartsWith(authConfig.AuthDirectory))
                    {
                        // Need auth
                        var authString = "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(authConfig.UserName + ":" + authConfig.Password));
                        if (request.Object.Authorization == authString)
                        {
                            return NextHandler != null ? NextHandler.Handle(request) : new NotImplemented();
                        }
                        else
                        {
                            var httpHeaders = new HttpHeaders();
                            httpHeaders.Add("WWW-Authenticate", "Basic realm=\"" + authConfig.AuthName + "\"");
                            return new Unauthorized(httpHeaders, null);
                        }
                    }
                }
            }
            return NextHandler != null ? NextHandler.Handle(request) : new NotImplemented();
        }
    }
}
