using System;
using System.IO;
using Gepard.Core.Main;

namespace Gepard.Core.Logs
{
    public class AccessLogHandler : ILogHandler
    {
        public string Path { get; set; }
        public string Ip { get; set; }
        public string UserAgent { get; set; }

        public AccessLogHandler(string path, string ip, string userAgent)
        {
            Path = path;
            Ip = ip;
            UserAgent = userAgent;
        }

        public string GetTime()
        {
            return DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
        }

        public void WriteInfo(string message)
        {
            while (true)
            {
                try
                {
                    File.AppendAllLines(Path, new[] { $"[{GetTime()}] [{Ip}] [{UserAgent}] {message}" });
                    return;
                }
                catch
                {
                    // ignored
                }
            }
        }
    }
}
