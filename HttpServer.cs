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
using Gepard.Controllers;

namespace Gepard
{
    public class HttpServer
    {
        public const string HttpVersion = "HTTP/1.1";

        private TcpListener Server { get; }
        private Thread Tasker { get; set; }
        private ServerConfig ServerConfig { get; }
        private VirtualHostList VirtualHostList { get; set; }
        private HttpProcessor HttpProcessor { get; set; }
        private ControllerRegistry ControllerRegistry { get; set; }
        
        public HttpServer(ServerConfig serverConfig, VirtualHostList virtualHostList, ControllerRegistry controllerRegistry)
        {
            ServerConfig = serverConfig;
            VirtualHostList = virtualHostList;
            ControllerRegistry = controllerRegistry;
            Server = new TcpListener(IPAddress.Parse(ServerConfig.Ip), ServerConfig.Port);
            HttpProcessor = new HttpProcessor(ServerConfig, VirtualHostList);
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
                Console.WriteLine("-> Client" + client.Client.RemoteEndPoint + " connected.");
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
                    var strRequest = Receive(stream);
                    var request = Request.Parse(strRequest);
                    request.VirtualHost = VirtualHostList.GetVirtualHost(request.Host);

                    IController controller;
                    Response response;

                    try
                    {
                        controller = ControllerRegistry.Get(request.Method);
                    }
                    catch (Exception e)
                    {
                        
                    }


                    response = HttpProcessor.GetResponse(request);
                    HttpProcessor.PushTo(stream, response);

//                    Console.WriteLine("Client " + client.Client.RemoteEndPoint + " disconnected.");
                    client.Close();
                    break;
                }
                catch (Exception e)
                {
//                    Console.WriteLine("Client " + client.Client.RemoteEndPoint + " disconnected.");
//                    client.Close();
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

//        private static IPAddress GetLocalIp()
//        {
//            var host = Dns.GetHostEntry(Dns.GetHostName());
//            foreach (var ip in host.AddressList)
//            {
//                if (ip.AddressFamily == AddressFamily.InterNetwork)
//                {
//                    return ip;
//                }
//            }
//            throw new Exception("Local IP Address Not Found!");
//        }
    }
}
