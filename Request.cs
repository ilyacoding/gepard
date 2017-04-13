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
        public string Host { get; set; }
        public string Connection { get; set; }
        public VirtualHost VirtualHost { get; set; }
        
        public Dictionary<string, string> Fields { get; set; }

        public Request(string method, Uri uri, string httpVersion, Dictionary<string, string> fields)
        {
            Method = method;
            Uri = uri;
            HttpVersion = httpVersion;

            Fields = fields;

            if (fields.ContainsKey("Connection"))
            {
                Connection = fields["Connection"];
            }

            if (fields.ContainsKey("Host"))
            {
                Host = fields["Host"].Split(':')[0];
            }
        }

        public static Request Parse(string data)
        {
            var parts = data.Split('\n').ToList();
            if (parts.Count < 1) throw new Exception();
            var startingLine = parts[0].Split(' ');
            parts.RemoveAt(0);

            if (startingLine.Length != 3) throw new Exception();
            var method = startingLine[0];
            var uri = Uri.Parse(startingLine[1]);
            var httpVersion = startingLine[2];
            
            var fields = parts.Select(p => p.Split(new[] {':'}, 2)).Where(f => f.Count() > 1).ToDictionary(f => f[0].Trim(), f => f[1].Trim());

            return new Request(method, uri, httpVersion, fields);
        }
    }
}
