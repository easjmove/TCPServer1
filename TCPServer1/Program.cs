using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("TCP Server 1");

TcpListener listener = new TcpListener(IPAddress.Any, 7);

listener.Start();
while (true)
{
    TcpClient socket = listener.AcceptTcpClient();
    IPEndPoint clientEndPoint = socket.Client.RemoteEndPoint as IPEndPoint;
    Console.WriteLine("Client connected: " + clientEndPoint.Address);

    NetworkStream ns = socket.GetStream();
    StreamReader reader = new StreamReader(ns);
    StreamWriter writer = new StreamWriter(ns);

    while (socket.Connected)
    {
        string? message = reader.ReadLine();
        Console.WriteLine(message);
        writer.WriteLine(message);
        writer.Flush();

        if (message == "stop")
        {
            socket.Close();
        }
    }
}

listener.Stop();

