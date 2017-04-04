using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gepard
{
    public class VirtualHost
    {
        public Dictionary<string, string> Fields { get; set; }

        public VirtualHost(string[] lines)
        {
//            foreach(var line in lines)
//            {
//                var data = line.Split(new char[] { ':' });
//                if (data.Count() > 1)
//                {
//                    Fields.Add(data[0].Trim(), data[1].Trim());
//                }
//            }
        }
    }
}
