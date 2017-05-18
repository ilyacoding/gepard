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
using Gepard.Core;

namespace Gepard
{
    public class HttpServer
    {
        public static string HttpServerName { get; set; }

        private TcpListener Server { get; }
        private Task Task { get; set; }
        private ServerConfig ServerConfig { get; }
        private VirtualHostList VirtualHostList { get; set; }
        private ControllerHandler ControllerHandler { get; set; }
        
        public HttpServer(ServerConfig serverConfig, VirtualHostList virtualHostList, ControllerHandler controllerHandler)
        {
            HttpServerName = serverConfig.ServerName;
            ServerConfig = serverConfig;
            VirtualHostList = virtualHostList;
            ControllerHandler = controllerHandler;

            Server = new TcpListener(IPAddress.Parse(ServerConfig.Ip), ServerConfig.Port);
            Task = new Task(AcceptBackground);
        }

        public void Start()
        {
            Server.Start();
            Task.Start();
            Console.WriteLine("Server started at " + Server.LocalEndpoint);
        }

        public void Stop()
        {
         //   Task.Dispose();
        }

        public void AcceptBackground()
        {
            while (true)
            {
                var client = Server.AcceptTcpClient();
                //Console.WriteLine("-> Client" + client.Client.RemoteEndPoint + " connected.");
                //var task = new Task((() => HandleClient(client)));//Task.Factory.StartNew(()=>HandleClient(client));
                Task.Factory.StartNew(() => HandleClient(client));
            }
        }
       
        public void HandleClient(TcpClient client)
        {
            var stream = client.GetStream();
            client.ReceiveTimeout = ServerConfig.KeepAliveTimeout * 1000;

            while (true)
            {
                try
                {
                    var strRequest = Receive(stream);
                    var response = ControllerHandler.Execute(strRequest, client.Client.LocalEndPoint.ToString());
                    Console.WriteLine(Encoding.UTF8.GetString(response.ArrayBytes));
                    Send(stream, response.ArrayBytes);
                    
                    if (response.ConnectionAlive) continue;
                    
                    client.Close();
                    break;
                }
                catch (Exception e)
                {
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

        public void Send(NetworkStream stream, byte[] msg)
        {
            stream.Write(msg, 0, msg.Length);
        }
    }
}
