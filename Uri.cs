using System;
using System.Collections.Generic;
using System.Linq;

namespace Gepard
{
    public class Uri
    {
        public string Url { get; set; }
        public Dictionary<string, string> UriDictionary { get; set; }
        
        public Uri(string uri)
        {
            UriDictionary = new Dictionary<string, string>();
            uri = uri.Trim('/', '\\');

            var uriParts = uri.Split('?');
            Url = uriParts[0];

            if (uriParts.Length > 2) throw new Exception("Invalid url");

            if (uriParts.Length != 2) return;

            var parameters = uriParts[1].Split('&');
            UriDictionary = parameters.Select(element => element.Split('=')).Where(keyValue => keyValue.Length == 2).ToDictionary(keyValue => keyValue[0].Trim(), keyValue => keyValue[1].Trim());
        }
    }
}
