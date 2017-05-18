using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gepard.Core.Logs;

namespace Gepard
{
    public class ErrorHandler
    {
        public string Path { get; set; }

        public ErrorHandler(string path)
        {
            Path = path;
        }

        private static string GetTime()
        {
            return DateTime.Now.ToString("[dd/MM/yyyy | h:mm:ss] ");
        }

        public void WriteError(string message)
        {
            File.AppendAllLines(Path, new[] { GetTime() + message });
        }

        public void WriteCriticalError(string message)
        {
            WriteError(message);
            throw new Exception(message);
        }
    }
}
