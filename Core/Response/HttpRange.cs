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

        public HttpRange(string str)
        {
            //"bytes=88080384-";
            var p = str.Split(new [] { ',' })[0].Split(new [] { ' ' })[1];
            var parsed = p.Split(new [] { '-' });
            Min = Convert.ToInt32(parsed[0] ?? "0");
            if (parsed.Length > 1)
            {
                Max = Convert.ToInt32(parsed[1] != "" ? parsed[1] : "0");
            }
        }

        public override string ToString()
        {
            return "bytes " + Min + "-" + Max + "/" + Size;
        }
    }
}
