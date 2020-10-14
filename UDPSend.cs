
////
////  -----------------------
////  UDP - Send
////  ---------------------- -
////  [url]http://msdn.microsoft.com/de-de/library/bb979228.aspx#ID0E3BAC[/url]
   
////  basic code taken from here: https://forum.unity.com/threads/simple-udp-implementation-send-read-via-mono-c.15900/
//// 
////
   
    
using System.Collections;

using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;


public class UDPSend
{
    private static int localPort;
    private string locationString;

    // prefs
    private string IP;  // define in init
    public int port;  // define in init

    // "connection" things
    IPEndPoint remoteEndPoint;
    UdpClient client;

    // gui
    string strMessage = "";
  
    // init
    public void init()
    {
        // Endpunkt definieren, von dem die Nachrichten gesendet werden.
        Console.WriteLine("UDPSend.init()");

        // I setup a NiIP DDNS for my router, which can be accessed from the internet to then check for the current externally visible IP
        IPAddress[] res = Dns.GetHostEntry("nakamir.mynetgear.com").AddressList;

        // use this port for UDP (port exception added to firewall and router port forwarding!)
        port = 8051;

        remoteEndPoint = new IPEndPoint(res[0], port);
        // this is only used if the IP is specified directly as string
        //remoteEndPoint = new IPEndPoint(IPAddress.Parse(IP), port);

        // some debug messages
        //print("Sending to " + res[0].ToString() + " : " + port);
        //print("Testing: nc -lu " + res[0].ToString() + " : " + port);
        //debugText.text = "Sending to " + res[0].ToString() + " : " + port.ToString();

        client = new UdpClient();
    }

    // send new Location
    public void SendLocation( string locationString )
    {
        try
        {
            // Daten mit der UTF8-Kodierung in das Binärformat kodieren.
            byte[] data = Encoding.UTF8.GetBytes(locationString);

            // Den message zum Remote-Client senden.
            client.Send(data, data.Length, remoteEndPoint);

        }
        catch (Exception err)
        {
            Console.WriteLine( err.ToString() );

        }
    }
}

