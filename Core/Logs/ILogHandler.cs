using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gepard.Core.Logs
{
    public interface ILogHandler
    {
        string Path { get; set; }
        string Ip { get; set; }
        string UserAgent { get; set; }

        string GetTime();
        void WriteInfo(string message);
    }
}
