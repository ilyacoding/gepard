using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Gepard.Configuration.Server;
using Gepard.Configuration.VirtualHost;
using Gepard.Data;

namespace Gepard
{
    public class HttpServer
    {
        private TcpListener Server { get; }
        private Thread Tasker { get; set; }

        public HttpServer(ServerConfig serverConfig, VirtualHostList virtualHostList)
        {
            Server = new TcpListener(GetLocalIp(), serverConfig.Port);
            Server.Start();
            Console.WriteLine(Server.LocalEndpoint);
            StartAccepting();
        }

        public void StartAccepting()
        {
            Tasker = new Thread(AcceptBackground);
            Tasker.Start();
        }

        public void EndAccepting()
        {
            Tasker.Abort();
        }

        public void AcceptBackground()
        {
            while (true)
            {
                var client = Server.AcceptTcpClient();
                Console.WriteLine("-> Client connected.");
                var newThread = new Thread(HandleClient);
                newThread.Start(client);
            }
        }
       
        public void HandleClient(object obj)
        {
            var client = (TcpClient)obj;
            while (true)
            {
                try
                {
                    var request = Receive(client.GetStream());

                    var html = request;
                    var str = "HTTP/1.1 200 OK\nContent-type: text/html\nContent-Length:" + html.Length.ToString() + "\n\n" + html;
                    
                    Send(client.GetStream(), str);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    break;
                }
            }
        }
        
        public string Receive(NetworkStream stream)
        {
            var buffer = new byte[256];
            var data = "";

            while (stream.DataAvailable || data.Length == 0)
            {
                var bytesRead = stream.Read(buffer, 0, buffer.Length);
                data += Encoding.UTF8.GetString(buffer, 0, bytesRead);
            }

            return data;
        }

        public void Send(NetworkStream stream, string msg)
        {
            var buffer = Encoding.UTF8.GetBytes(msg);
            stream.Write(buffer, 0, buffer.Length);
        }

        private static IPAddress GetLocalIp()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip;
                }
            }
            throw new Exception("Local IP Address Not Found!");
        }
    }
}
