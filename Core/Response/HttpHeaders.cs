using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gepard.Core.Response
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
