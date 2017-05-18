using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gepard.Core.Response
{
    public class HttpRange
    {
        public ulong Min { get; private set; }
        public ulong Max { get; private set; }
        public ulong Size { get; private set; }

        public HttpRange(string str)
        {
            var parsed = str.Split(new[] { ',' })[0].Split(new[] { '=' })[1].Split(new [] { '-' }, 2);
            Min = Convert.ToUInt64(parsed[0] != "" ? parsed[0] : "0");
            Max = Convert.ToUInt64(parsed[1] != "" ? parsed[1] : ulong.MaxValue.ToString());

            if (Min > Max)
            {
                throw new Exception();
            }
        }

        public void Normalize(ulong size)
        {
            Size = size;

            if (Max > Size)
            {
                Max = Size;
            }
        }

        public override string ToString()
        {
            return "bytes " + Min + "-" + Max + "/" + Size;
        }
    }
}
