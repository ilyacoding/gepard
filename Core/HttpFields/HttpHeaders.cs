using System.Collections.Generic;

namespace Gepard.Core.HttpFields
{
    public class HttpHeaders
    {
        public Dictionary<string, string> HeadersList { get; }
 
        public HttpHeaders()
        {
            HeadersList = new Dictionary<string, string>();
        }

        public void Add(string key, string value)
        {
            HeadersList.Add(key, value);
        }
    }
}
