using System;
using System.Collections.Generic;
using System.Linq;
using Gepard.Configuration.VirtualHost;
using Gepard.Core.Response;

namespace Gepard.Core.Request
{
    public class Request
    {
        public string Method { get; set; }
        public Uri Uri { get; set; }
        public string HttpVersion { get; set; }

        public string Host { get; set; }
        public string Authorization { get; set; }
        public string UserAgent { get; set; }
        public bool KeepAlive { get; set; }
        //public HttpRange HttpRange { get; set; }

        private Dictionary<string, string> Fields { get; set; }
        public string this[string key] => Fields.ContainsKey(key.ToLower()) ? Fields[key.ToLower()] : null;

        public string Body { get; set; }

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

            Fields = headers.Select(p => p.Split(new[] { ':' }, 2)).Where(f => f.Count() > 1).ToDictionary(f => f[0].Trim().ToLower(), f => f[1].Trim());

            Host = this["Host"].Split(new[] {':'})[0];

            Authorization = this["Authorization"] ?? "";
            UserAgent = this["User-Agent"] ?? "";

            if (this["Connection"] != null)
            {
                KeepAlive = string.Equals(this["Connection"], "keep-alive", StringComparison.CurrentCultureIgnoreCase);
            }

            //try
            //{
            //    HttpRange = new HttpRange(this["Range"]);
            //}
            //catch (Exception)
            //{
            //    //Console.WriteLine(e);
            //}
        }
    }
}
