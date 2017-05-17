using System;
using System.Collections.Generic;
using System.Linq;
using Gepard.Configuration.VirtualHost;

namespace Gepard
{
    public class Request
    {
        public string Method { get; set; }
        public Uri Uri { get; set; }
        public string HttpVersion { get; set; }

        public Dictionary<string, string> Fields { get; set; }
        public string this[string key] => Fields.ContainsKey(key) ? Fields[key] : null;

        public string Body { get; set; }
        public string Host { get; set; }

        public VirtualHost VirtualHost { get; set; }
        
        public Request(string data)
        {
            var parts = data.Split(new[] { "\r\n\r\n" }, StringSplitOptions.None);
            if (parts.Length < 1) throw new Exception("Invalid data");

            Body = parts.Length == 2 ? parts[1] : "";

            var headers = parts[0].Split('\n').ToList();

            var startingLine = headers[0].Split(' ');
            headers.RemoveAt(0);

            if (startingLine.Length != 3) throw new Exception();
            Method = startingLine[0];
            Uri = new Uri(startingLine[1]);

            Fields = headers.Select(p => p.Split(new[] { ':' }, 2)).Where(f => f.Count() > 1).ToDictionary(f => f[0].Trim(), f => f[1].Trim());

            Host = this["Host"].Split(new[] {':'})[0];
        }
    }
}
