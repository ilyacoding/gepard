using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gepard.Core.FileHandling;
using Gepard.Core.HttpAction;
using Gepard.Core.HttpFields;
using Gepard.Core.HttpHelpers;
using Gepard.Core.Main;
using Gepard.Core.Requests;

namespace Gepard.Controllers
{
    public class PostMethodHandler : IRequestHandler
    {
        public IRequestHandler NextHandler { get; set; }
        
        public IHttpAction Handle(Request request)
        {
            if (request.Object.Method == "POST")
            {
                if (request.Object.Body.Length > 0 && request.Object["Content-Type"] == "application/x-www-form-urlencoded")
                {
                    var httpHeaders = new HttpHeaders();

                    try
                    {
                        var httpUrlEncoded = new HttpUrlEncoded(request.Object.Body);
                        var responseString = "";
                        foreach (var keyValue in httpUrlEncoded.Dictionary)
                        {
                            responseString += $"[\"{keyValue.Key}\" => \"{keyValue.Value}\"], ";
                        }

                        httpHeaders.Add("Content-Type", "text/html");
                        return new Ok(httpHeaders, Encoding.UTF8.GetBytes(responseString));
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        return new BadRequest();
                    }
                }

                return new BadRequest();
            }
            return NextHandler != null ? NextHandler.Handle(request) : new NotImplemented();
        }
    }
}
