using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Gepard.Configuration.Server;
using Gepard.Configuration.VirtualHost;

namespace Gepard
{
    public class TcpServer
    {
        public const string HttpVersion = "HTTP/1.1";

        private TcpListener Server { get; }
        private Thread Tasker { get; set; }
        private ServerConfig ServerConfig { get; }
        private VirtualHostList VirtualHostList { get; set; }
        
        public TcpServer(ServerConfig serverConfig, VirtualHostList virtualHostList)
        {
            ServerConfig = serverConfig;
            VirtualHostList = virtualHostList;
            Server = new TcpListener(IPAddress.Parse(ServerConfig.Ip), ServerConfig.Port);
        }

        public void Start()
        {
            Server.Start();
            Tasker = new Thread(AcceptBackground);
            Tasker.Start();
            Console.WriteLine("Server started at " + Server.LocalEndpoint);
        }

        public void Stop()
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
            var stream = client.GetStream();
            while (true)
            {
                try
                {
                    var html = Receive(stream);

                    var request = Request.Parse(html);

                    var response = Response.From(request, ServerConfig);
                    response.Post(stream);
//                    \r\nAccept - Ranges: bytes

                    //                    response.Post(stream);
                    //                    var str = "HTTP/1.1 200 OK\nContent-type: text/html\nContent-Length:" + html.Length.ToString() + "\n\n" + html;
                    //                    

                    if (request.Connection.Equals("keep-alive")) continue;

                    Console.WriteLine("Client " + client.Client.RemoteEndPoint + " disconnected.");
                    client.Close();
                    break;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Client " + client.Client.RemoteEndPoint + " disconnected.");
                    client.Close();
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
