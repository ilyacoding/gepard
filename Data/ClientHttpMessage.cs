using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gepard.Data
{
    public class ClientHttpMessage : IHttpMessage
    {
        public string Method { get; private set; }
        public string URI { get; private set; }
        public string HttpVersion { get; private set; }

        public Dictionary<string, string> fields { get; set; }

        public string Host { get; private set; }
        public string UserAgent { get; private set; }
        public string Accept { get; private set; }
        public string AcceptLanguage { get; private set; }
        public string AcceptEncoding { get; private set; }
        public string Connection { get; set; }
        public string CacheControl { get; private set; }

        public string Content { get; set; }
       
        public ClientHttpMessage(string data)
        {
            fields = new Dictionary<string, string>();
            List<string> parts = data.Split(new char[] { '\n' }).ToList();
            var StartingLine = parts[0].Split(' ');
            parts.RemoveAt(0);

            Method = StartingLine[0];
            URI = StartingLine[1];
            HttpVersion = StartingLine[2];

            foreach(var p in parts)
            {
                string[] f = p.Split(new char[] { ':' }, 2);
                if (f.Count() > 1)
                {
                    fields.Add(f[0].Trim(), f[1].Trim());
                }
            }

            foreach(var x in fields)
            {
                Console.WriteLine(x.Key + "=>" + x.Value);
            }
            if (fields["User-Agent"] != null)
            {
                Console.WriteLine(fields["User-Agent"]);
            }
        }

        public override string ToString()
        {
            return "";
        }
    }
}
