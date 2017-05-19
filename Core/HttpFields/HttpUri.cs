using System;
using System.Collections.Generic;
using System.Linq;

namespace Gepard.Core.HttpFields
{
    public class HttpUri
    {
        public string Url { get; set; }
        public HttpUrlEncoded HttpUrlEncoded { get; set; }
        public Dictionary<string, string> UriDictionary { get; set; }
        
        public HttpUri(string uri)
        {
            UriDictionary = new Dictionary<string, string>();
            uri = uri.Trim('/', '\\');

            var uriParts = uri.Split('?');
            Url = uriParts[0];

            if (uriParts.Length > 2) throw new Exception("Invalid url");

            if (uriParts.Length != 2) return;

            try
            {
                HttpUrlEncoded = new HttpUrlEncoded(uriParts[1]);
            }
            catch
            {
                // ignored
            }
        }
    }
}
