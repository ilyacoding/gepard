using System.Collections.Generic;
using System.Linq;

namespace Gepard.Core.HttpFields
{
    public class HttpUrlEncoded
    {
        public Dictionary<string, string> Dictionary { get; set; }

        public HttpUrlEncoded(string str)
        {
            var parameters = str.Split('&');
            Dictionary = parameters.Select(element => element.Split('=')).Where(keyValue => keyValue.Length == 2).ToDictionary(keyValue => keyValue[0].Trim(), keyValue => keyValue[1].Trim());
        }
    }
}
