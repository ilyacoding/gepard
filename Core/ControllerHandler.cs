﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gepard.Configuration.Server;
using Gepard.Configuration.VirtualHost;

namespace Gepard.Core
{
    public class ControllerHandler
    {
        private ChainControllerHandler ChainControllerHandler { get; set; }
        private VirtualHostList VirtualHostList { get; set; }

        public ControllerHandler(VirtualHostList virtualHostList, ChainControllerHandler chainControllerHandler)
        {
            VirtualHostList = virtualHostList;
            ChainControllerHandler = chainControllerHandler;
        }

        public byte[] Execute(string str)
        {
            var request = new Request(str);
            request.VirtualHost = VirtualHostList.GetVirtualHost(request.Host);

            var httpResponse = ChainControllerHandler.Execute(request).HttpResponse;

            return httpResponse.GetBytes();
        }
    }
}
