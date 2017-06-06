using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Linq;

namespace EOPenServer
{
    internal class SynchronousSocketListener
    {
        // Incoming data from the client.  
        public static string data = null;

        public static void StartListening() {
            // Data buffer for incoming data.  
            byte[] bytes = new Byte[1024];

            // Establish the local endpoint for the socket.  
            // Dns.GetHostName returns the name of the   
            // host running the application.              
            IPHostEntry ipHostInfo = Dns.GetHostEntry(""); // AddressFamily.InterNetwork
            //IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPAddress ipAddress = null;
            //IPAddress[] validAddressList = new IPAddress[ipHostInfo.AddressList.Length];
            //IPAddressList validAddressList = new IPAddressList(ipHostInfo.AddressList);
            //IPAddressFilteredList validAddressList = new IPAddressFilteredList(ipHostInfo.AddressList, AddressFamily.InterNetwork);                       

            //IEnumerable<IPAddress> validAddressList = ipHostInfo.AddressList.Where(x => x.AddressFamily == AddressFamily.InterNetwork);
            IList<IPAddress> validAddressList = ipHostInfo.AddressList.Where(x => x.AddressFamily == AddressFamily.InterNetwork).ToList<IPAddress>();


            //{
            //    int n = 0;
            //    for (int i = 0; i < ipHostInfo.AddressList.Length; i++) {
            //        if (ipHostInfo.AddressList[i].AddressFamily == AddressFamily.InterNetwork) {
            //            validAddressList[n++] = ipHostInfo.AddressList[i];
            //            // break;
            //        }
            //    }
            //}

            //if (validAddressList.Length > 1) {
            //    for (int i = 0; i < validAddressList.Length; i++){
            //        if (validAddressList[i] == null)
            //            break;
            //        Console.WriteLine(string.Format("{0} - {1}", (i+1).ToString(), validAddressList[i].ToString()));
            //    }
            //    Console.WriteLine("En qué dirección quieres levantar el server: ");
            //    string response = Console.ReadLine();
            //    int intRes = int.Parse(response);
            //    ipAddress = validAddressList[intRes-1];
            //}
            //else {
            //    ipAddress = validAddressList[0];
            //}

            Console.WriteLine("Numero de interfaces: " + validAddressList.Count);
            int i = 0;
            //foreach (object obj in validAddressList) {
                //IPAddress current = (IPAddress)obj;
            foreach (IPAddress current in validAddressList) {                
                Console.WriteLine(string.Format("{0} - {1}", (++i).ToString(), current.ToString()));
            }

            Console.WriteLine("En qué dirección quieres levantar el server: ");
            string response = Console.ReadLine();
            int intRes = int.Parse(response);
            ipAddress = validAddressList[intRes - 1];
            //return;
            
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 4510);
            Console.WriteLine("IP Address: " + ipAddress.ToString());

            // Create a TCP/IP socket.  
            Socket listener = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);

            // Bind the socket to the local endpoint and   
            // listen for incoming connections.  
            try {
                listener.Bind(localEndPoint);
                listener.Listen(10);

                // Start listening for connections.  
                while (true) {
                    Console.WriteLine("Waiting for a connection...");
                    // Program is suspended while waiting for an incoming connection.  
                    Socket handler = listener.Accept();
                    data = null;

                    // An incoming connection needs to be processed.  
                    while (true) {
                        bytes = new byte[1024];
                        int bytesRec = handler.Receive(bytes);
                        data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                        if (data.IndexOf("<EOF>") > -1) {
                            break;
                        }
                    }

                    // Show the data on the console.  
                    Console.WriteLine("Text received : {0}", data);

                    // Echo the data back to the client.  
                    byte[] msg = Encoding.ASCII.GetBytes(data);

                    handler.Send(msg);
                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                }

            }
            catch (Exception e) {
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine("\nPress ENTER to continue...");
            Console.Read();

        }  
    }
}
