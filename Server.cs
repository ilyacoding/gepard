using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Configuration;

namespace Gepard
{
    class Server
    {
        private TCPServer serv { get; set; }

        public Server(IPAddress ip, int port)
        {
            serv = new TCPServer(ip, port);
            serv.StartAccepting();
        }

        static void Main(string[] args)
        {
            new Server(IPAddress.Parse(ConfigurationSettings.AppSettings["ServerIP"]), Convert.ToInt16(ConfigurationSettings.AppSettings["ServerPort"]));
        }
    }
}
