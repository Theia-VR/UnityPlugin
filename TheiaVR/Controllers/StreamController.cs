using System;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Threading;
using TheiaVR.Helpers;

namespace TheiaVR.Controllers
{
    /**
     * Class allowing to listen UDP stream from the Kinect streamer
     */
    public class StreamController
    {
        private static StreamController instance = null;
        
        // Kinect listener instance
        private Thread listener;

        // Used to secure listening infinite loop
        private bool listening;
        
        private StreamController()
        {
            // Used to prevent public access
        }

        public static StreamController GetInstance()
        {
            if(instance == null)
            {
                instance = new StreamController();
            }
            return instance;
        }
        
        public void Start(string aHost, int aPort)
        {
            if (IsActive())
            {
                throw new Exception("Already connected to UDP stream");
            }

            IPAddress vAddress = null;

            Regex vIp = new Regex(@"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b");

            if (vIp.IsMatch(aHost))
            {
                vAddress = IPAddress.Parse(aHost);
            }
            else
            {
                var vAddresses = Dns.GetHostAddresses(aHost);
                if (vAddresses.Length == 0)
                {
                    throw new Exception("Unable to retrieve address from specified host name : " + aHost);
                }
                else if (vAddresses.Length > 1)
                {
                    throw new Exception("There is more that one IP address to the specified host : " + aHost);
                }
                vAddress = vAddresses[0];
            }

            listener = new Thread(Listen);
            listening = true;
            listener.Start(new IPEndPoint(vAddress, aPort));
            
            Messages.Log("Kinect stream listener started");
        }

        public void Stop()
        {
            listening = false;
            listener.Abort();
            Messages.Log("Kinect stream listener stopped");
        }

        private void Listen(object aIPEndPoint)
        {
            IPEndPoint vIPEndPoint = (IPEndPoint)aIPEndPoint;
            UdpClient vStreamer = new UdpClient(vIPEndPoint.Port);

            try
            {
                while (listening)
                {
                    byte[] bytes = vStreamer.Receive(ref vIPEndPoint);
                    Messages.Log("UDP packet received");
                }
            }
            catch (Exception e)
            {
                Messages.LogError(e.ToString());
            }
            finally
            {
                vStreamer.Close();
            }
        }

        public bool IsActive()
        {
            return listener != null && listener.IsAlive && listening;
        }
    }
}
