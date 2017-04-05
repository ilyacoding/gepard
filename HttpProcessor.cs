using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Gepard
{
    public class HttpProcessor
    {
        public string RequestString { get; set; }
        public HttpProcessor(string Request)
        {
            RequestString = Request;
        }
    }
}
