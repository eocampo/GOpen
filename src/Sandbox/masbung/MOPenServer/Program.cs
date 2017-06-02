using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace MOPenServer
{
    class Program
    {
        static void Main(string[] args) {

            Console.WriteLine("GOpen Server 1.0.0.0");

            var permission = new SocketPermission(NetworkAccess.Accept,
                   TransportType.Tcp, "", SocketPermission.AllPorts);

            IPHostEntry ipHost = Dns.GetHostEntry(""); // AddressFamily.InterNetwork
            IPAddress ipAddr = null;
            for (int i = 0; i < ipHost.AddressList.LongLength; i++) {
                if (ipHost.AddressList[i].AddressFamily == AddressFamily.InterNetwork && ipHost.AddressList[i].ToString().StartsWith("192"))
                    ipAddr = ipHost.AddressList[i];
            }

            var ipEndPoint = new IPEndPoint(ipAddr, 4510);

            Socket sListener = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp); //  ipAddr.AddressFamily

            Console.WriteLine("IP Address: " + ipAddr.ToString());
        }
    }
}
