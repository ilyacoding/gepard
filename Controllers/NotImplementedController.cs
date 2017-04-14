﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gepard.Configuration.Server;
using Gepard.Core;

namespace Gepard.Controllers
{
    public class NotImplementedController : IController
    {
        public ServerConfig ServerConfig { get; set; }
        public FileHandler FileHandler { get; set; }

        public Response Execute(Request request)
        {
            throw new NotImplementedException();
        }
    }
}