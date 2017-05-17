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

        public Dictionary<string, string> Fields { get; set; }
        
        public Response(string status, byte[] data, string mime = "text/plain")
        {
            Status = status;
            Data = data;
            Mime = mime;

            Fields = new Dictionary<string, string>();
        }

        public byte[] GetBytes()
        { 
            if (Data != null)
            {
                Fields.Add("Accept-ranges", "bytes");
                Fields.Add("Content-Length", Data.Length.ToString());
            }

            var data = $"{HttpServer.HttpVersion} {Status}\r\n";

            foreach (var title in Fields)
            {
                data += $"{title.Key}: {title.Value}\r\n";
            }

            data += "\r\n";

            return Data != null ? CombineBytes(Encoding.UTF8.GetBytes(data), Data) : Encoding.UTF8.GetBytes(data);
        }

        private static byte[] CombineBytes(byte[] first, byte[] second)
        {
            var ret = new byte[first.Length + second.Length];
            Buffer.BlockCopy(first, 0, ret, 0, first.Length);
            Buffer.BlockCopy(second, 0, ret, first.Length, second.Length);
            return ret;
        }
    }
}
