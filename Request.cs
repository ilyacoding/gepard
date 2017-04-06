using System;
using System.Collections.Generic;
using System.Linq;

namespace Gepard
{
    public class Request
    {
        public string Method { get; set; }
        public string Uri { get; set; }
        public string HttpVersion { get; set; }
        public string Connection { get; set; }

        public Dictionary<string, string> Fields { get; set; }

        public Request(string method, string uri, string httpVersion, Dictionary<string, string> fields)
        {
            Method = method;
            Uri = uri;
            HttpVersion = httpVersion;
            Fields = fields;
            if (fields.ContainsKey("Connection"))
            {
                if (fields["Connection"].Contains("keep-alive"))
                {
                    Connection = fields["Connection"];
                }
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
            var uri = startingLine[1];
            var httpVersion = startingLine[2];

            var connection = false;

            var fields = parts.Select(p => p.Split(new[] {':'}, 2)).Where(f => f.Count() > 1).ToDictionary(f => f[0].Trim(), f => f[1].Trim());

//            foreach (var x in fields)
//            {
//                Console.WriteLine(x.Key + "=>" + x.Value);
//            }

            return new Request(method, uri, httpVersion, fields);
        }
    }
}
