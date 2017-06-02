﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace EOPenClient
{
    public class SynchronousSocketClient
    {
        public static void StartClient() {
            // Data buffer for incoming data.  
            byte[] bytes = new byte[1024];

            // Connect to a remote device.  
            try {
                // Establish the remote endpoint for the socket.  
                // This example uses port 11000 on the local computer.  
                //IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName())  
                IPHostEntry ipHostInfo = Dns.GetHostEntry(""); // AddressFamily.InterNetwork
                //IPAddress ipAddress = ipHostInfo.AddressList[0];
                IPAddress ipAddress = null;
                for (int i = 0; i < ipHostInfo.AddressList.LongLength; i++) {
                    if (ipHostInfo.AddressList[i].AddressFamily == AddressFamily.InterNetwork)
                        ipAddress = ipHostInfo.AddressList[i];
                }
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, 4510);

                // Create a TCP/IP  socket.  
                Socket sender = new Socket(AddressFamily.InterNetwork,
                    SocketType.Stream, ProtocolType.Tcp);

                // Connect the socket to the remote endpoint. Catch any errors.  
                try {
                    sender.Connect(remoteEP);

                    Console.WriteLine("Socket connected to {0}",
                        sender.RemoteEndPoint.ToString());

                    // Encode the data string into a byte array.  
                    byte[] msg = Encoding.ASCII.GetBytes("This is a test<EOF>");

                    // Send the data through the socket.  
                    int bytesSent = sender.Send(msg);

                    // Receive the response from the remote device.  
                    int bytesRec = sender.Receive(bytes);
                    Console.WriteLine("Echoed test = {0}",
                        Encoding.ASCII.GetString(bytes, 0, bytesRec));

                    // Release the socket.  
                    sender.Shutdown(SocketShutdown.Both);
                    sender.Close();

                }
                catch (ArgumentNullException ane) {
                    Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                }
                catch (SocketException se) {
                    Console.WriteLine("SocketException : {0}", se.ToString());
                }
                catch (Exception e) {
                    Console.WriteLine("Unexpected exception : {0}", e.ToString());
                }

            }
            catch (Exception e) {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
