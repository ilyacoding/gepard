using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Gepard.Controllers;
using Gepard.Core.HttpAction;

namespace Gepard.Core
{
    public class ChainControllerHandler
    {
        private IRequestHandler LastRequestHandler { get; set; }
        private IRequestHandler RequestHandler { get; set; }

        //public ChainControllerHandler()
        //{
        //    var type = typeof(IRequestHandler);
        //    var list = Assembly.GetAssembly(type).GetTypes().Where(t => t.IsSubclassOf(type));
        //
        //    foreach (var requestHandler in list)
        //    {
        //        Reg(Activator.CreateInstance(requestHandler) as IRequestHandler);
        //    }
        //}

        public IHttpAction Execute(Request.HttpRequest request)
        {
            return RequestHandler.Handle(request);
        }

        public void Reg(IRequestHandler requestHandler)
        {
            if (RequestHandler == null)
            {
                RequestHandler = requestHandler;
                LastRequestHandler = requestHandler;
                return;
            }

            LastRequestHandler.NextHandler = requestHandler;
            LastRequestHandler = requestHandler;
        }
    }
}
