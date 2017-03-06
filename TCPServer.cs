using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

using Gepard.Data;

namespace Gepard
{
    class TCPServer
    {
        public Socket sock { get; set; }
        private IPEndPoint localIP { get; set; }
        private Thread Tasker { get; set; }

        public TCPServer(IPAddress ip, int port)
        {
            sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            localIP = new IPEndPoint(ip, port);
            sock.Bind(localIP);
            sock.Listen(100);
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
                Socket handler = sock.Accept();
                Console.WriteLine("-> Client connected.");
                Thread newThread = new Thread(HandleClient);
                newThread.Start(handler);
            }
        }
       
        public void HandleClient(Object obj)
        {
            Socket handler = (Socket)obj;
            while (true)
            {
                try
                {
                    var data = ReceiveString(handler);
                    new ClientHttpMessage(data);

                    string Html = "<html><body><h1>It works!</h1></body></html>";
                    string Str = "HTTP/1.1 200 OK\nContent-type: text/html\nContent-Length:" + (Html.Length+data.Length).ToString() + "\n\n" + Html + data;
                    byte[] msg = Encoding.ASCII.GetBytes(Str);

                    handler.Send(msg);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    break;
                }
            }
        }
        
        public string ReceiveString(Socket socket)
        {
            byte[] bytes = new Byte[256];
            string data = "";
            int bytesReceived;

            do
            {
                bytesReceived = socket.Receive(bytes, 0, bytes.Length, SocketFlags.None);
                data += Encoding.UTF8.GetString(bytes, 0, bytesReceived);
                //Console.WriteLine(data);
            }
            while (socket.Available > 0);

            return data;
        }

        private IPAddress GetLocalIP()
        {
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
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
