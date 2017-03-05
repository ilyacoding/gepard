using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

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
                    byte[] bytes = new Byte[1024];
                    string data;

                    int bytesRec = handler.Receive(bytes);
                    data = Encoding.UTF8.GetString(bytes, 0, bytesRec);

                    //char[] array = data.ToCharArray();
                    //Array.Reverse(array);
                    //data = new string(array);

                    //byte[] msg = Encoding.UTF8.GetBytes(data);

                    string Html = "<html><body><h1>It works!</h1></body></html>";
                    string Str = "HTTP/1.1 200 OK\nContent-type: text/html\nContent-Length:" + Html.Length.ToString() + "\n\n" + Html;
                    byte[] msg = Encoding.ASCII.GetBytes(Str);

                    handler.Send(msg);
                    handler.Close();
                    break;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Client disconnected.");
                    break;
                }
            }
        }

        public void Send(string Message)
        {
            if (sock.Connected)
                sock.Send(Encoding.UTF8.GetBytes(Message));
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
