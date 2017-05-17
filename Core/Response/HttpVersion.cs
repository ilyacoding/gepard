using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gepard.Core.Response
{
    public class HttpVersion
    {
        public Version Version { get; set; }

        public HttpVersion()
        {
            Version = new Version(1, 1);
        }

        public override string ToString()
        {
            return "HTTP/" + Version;
        }
    }
}
