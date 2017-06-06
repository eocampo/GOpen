using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace EOPenServer
{
    class Program
    {
        static int Main(string[] args) {
            Console.WriteLine("GOpen Server 1.0.0.0");
            SynchronousSocketListener.StartListening();
            //MainTest.Run();
            return 0;
        }
    }
}
