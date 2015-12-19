using System.Net.Sockets;
using System.Net;
using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

class Server
{
    const string SERVER_IP = "127.0.0.1";
    const int  SERVER_PORT = 9998;
    TcpListener server = new TcpListener(IPAddress.Parse(SERVER_IP), SERVER_PORT);
    public void Start ()
    {
        server.Start();
        Console.WriteLine("Server has started on "+SERVER_IP+":"+SERVER_PORT+".{0}Waiting for a connection...", Environment.NewLine);
        TcpClient client;
        while (true) // Add your exit flag here
        {
            client = server.AcceptTcpClient();
            ThreadPool.QueueUserWorkItem(ThreadProc, client);
        }
        

       
        
    }

    private void ThreadProc(object obj)
    {
        Console.WriteLine("A client connected.");
        TcpClient client = (TcpClient)obj;
        NetworkStream stream = client.GetStream();
        //enter to an infinite cycle to be able to handle every change in stream
        while (true)
        {

            while (!stream.DataAvailable) ;

            Byte[] bytes = new Byte[client.Available];

            stream.Read(bytes, 0, bytes.Length);
            // translate bytes of request to string
            string data = Encoding.UTF8.GetString(bytes);

            if (new Regex("^GET").IsMatch(data)) // Handshaking protocol
            {
                Byte[] response = Encoding.UTF8.GetBytes("HTTP/1.1 101 Switching Protocols" + Environment.NewLine
                    + "Connection: Upgrade" + Environment.NewLine
                    + "Upgrade: websocket" + Environment.NewLine
                    + "Sec-WebSocket-Accept: " + Convert.ToBase64String(
                        SHA1.Create().ComputeHash(
                            Encoding.UTF8.GetBytes(
                                new Regex("Sec-WebSocket-Key: (.*)").Match(data).Groups[1].Value.Trim() + "258EAFA5-E914-47DA-95CA-C5AB0DC85B11"
                            )
                        )
                    ) + Environment.NewLine
                    + Environment.NewLine);

                stream.Write(response, 0, response.Length);

            }
            else
            {
                string msg = DecodeMessage(bytes);
                Console.WriteLine(msg);

            }

            //Console.WriteLine(ByteToString(bytes));
        }
    }

    private String DecodeMessage(Byte[] bytes)
    {
        String incomingData = String.Empty;
        Byte secondByte = bytes[1];
        Int32 dataLength = secondByte & 127;
        Int32 indexFirstMask = 2;
        if (dataLength == 126)
            indexFirstMask = 4;
        else if (dataLength == 127)
            indexFirstMask = 10;

        IEnumerable<Byte> keys = bytes.Skip(indexFirstMask).Take(4);
        Int32 indexFirstDataByte = indexFirstMask + 4;

        Byte[] decoded = new Byte[bytes.Length - indexFirstDataByte];
        for (Int32 i = indexFirstDataByte, j = 0; i < bytes.Length; i++, j++)
        {
            decoded[j] = (Byte)(bytes[i] ^ keys.ElementAt(j % 4));
        }

        return incomingData = Encoding.UTF8.GetString(decoded, 0, decoded.Length);
    }
    
}