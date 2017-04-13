using System;
using System.Collections.Generic;
using System.Linq;

namespace Gepard
{
    public class Uri
    {
        public string Url { get; set; }
        public Dictionary<string, string> UriDictionary { get; set; }

        public Uri(string url, Dictionary<string, string> uriDictionary)
        {
            Url = url;
            UriDictionary = uriDictionary;
        }

        public static Uri Parse(string uri)
        {
            var uriDictionary = new Dictionary<string, string>();
            uri = uri.Trim('/', '\\');

            var uriParts = uri.Split('?');
            var url = uriParts[0];

            if (uriParts.Length > 2) throw new Exception("Invalid url");

            if (uriParts.Length != 2) return new Uri(url, uriDictionary);

            var parameters = uriParts[1].Split('&');
            uriDictionary = parameters.Select(element => element.Split('=')).Where(keyValue => keyValue.Length == 2).ToDictionary(keyValue => keyValue[0].Trim(), keyValue => keyValue[1].Trim());

            return new Uri(url, uriDictionary);
        }
    }
}
