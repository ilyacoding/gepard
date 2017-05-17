﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Gepard;
using Gepard.Core;
using Gepard.Core.HttpAction;
using Gepard.Core.Response;

namespace Gepard.Controllers
{
    public class PostMethodHandler : IRequestHandler
    {
        public IRequestHandler NextHandler { get; set; }

        public IHttpAction Handle(Request request)
        {
            if (request.Method == "POST")
            {
                return new NotFound();
            }
            return NextHandler != null ? NextHandler.Handle(request) : new NotImplemented();
        }
    }
}
