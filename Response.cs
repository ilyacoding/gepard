using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Gepard.Configuration.Server;

namespace Gepard
{
    public class Response
    {
        public string Status { get; set; }
        public byte[] Data { get; set; }
        public string Mime { get; set; }

        public Response(string status, byte[] data, string mime)
        {
            Status = status;
            Data = data;
            Mime = mime;
        }
    }
}
