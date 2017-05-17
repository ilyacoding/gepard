using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gepard.Core.Response
{
    public class HttpRange
    {
        public int Min { get; set; }
        public int Max { get; set; }
        public int Size { get; set; }

        public override string ToString()
        {
            return "bytes " + Min + "-" + Max + "/" + Size;
        }
    }
}
