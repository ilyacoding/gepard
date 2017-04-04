using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gepard
{
    class VirtualHostHttpConfig
    {
        public List<VirtualHost> VirtualHosts;

        public VirtualHostHttpConfig(string filePath)
        {
            string[] ConfigContent = System.IO.File.ReadAllLines(filePath);
            var buffer = new List<string>();
            bool ParsingVHost = false;
            foreach(var line in ConfigContent)
            {
                if (line.Equals("<VirtualHost>"))
                {
                    ParsingVHost = true;
                    continue;
                }
                else if (line.Equals("</VirtualHost>"))
                {
                    ParsingVHost = false;
                    VirtualHosts.Add(new VirtualHost(buffer.ToArray()));
                    buffer.Clear();
                }

                if (ParsingVHost)
                {
                    buffer.Add(line);
                }
            }
        }
    }
}
