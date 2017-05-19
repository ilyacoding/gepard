using System;
using System.Text;
using Gepard.Core;
using Gepard.Core.HttpAction;
using Gepard.Core.HttpFields;
using Gepard.Core.Main;
using Gepard.Core.Requests;

namespace Gepard.Controllers
{
    public class BasicAuthHandler : IRequestHandler
    {
        public IRequestHandler NextHandler { get; set; }

        public IHttpAction Handle(Request request)
        {
            if (request.VirtualHost.BasicAuthConfigs != null)
            {
                foreach (var authConfig in request.VirtualHost.BasicAuthConfigs)
                {
                    if (request.Object.Uri.Url.StartsWith(authConfig.AuthDirectory))
                    {
                        // Need auth
                        var authValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(authConfig.UserName + ":" + authConfig.Password));

                        if (request.Object.Authorization.AuthType == "Basic" 
                            && request.Object.Authorization["Value"] == authValue)
                        {
                            return NextHandler != null ? NextHandler.Handle(request) : new NotImplemented();
                        }
                        else
                        {
                            var httpHeaders = new HttpHeaders();
                            httpHeaders.Add("WWW-Authenticate", "Basic realm=\"" + authConfig.Realm + "\"");
                            return new Unauthorized(httpHeaders, null);
                        }
                    }
                }
            }
            return NextHandler != null ? NextHandler.Handle(request) : new NotImplemented();
        }
    }
}
