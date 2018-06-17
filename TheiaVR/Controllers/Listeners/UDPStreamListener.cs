using System;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Threading;
using TheiaVR.Helpers;

namespace TheiaVR.Controllers.Listeners
{
    public abstract class UdpStreamListener : IStreamListener
    {
		//each listener is a Thread
        private Thread listener;

        private bool listening = false;

		//Start listner in common
        public void Start(string aHost, int aPort)
        {
            if (IsActive())
            {
                throw new PluginException("Already connected to UDP stream");
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
                    throw new PluginException("Unable to retrieve address from specified host name : " + aHost);
                }
                else if (vAddresses.Length > 1)
                {
                    throw new PluginException("There is more that one IP address to the specified host : " + aHost);
                }
                vAddress = vAddresses[0];
            }

            listener = new Thread(Listen);
            listener.IsBackground = true;
            listening = true;
            listener.Start(new IPEndPoint(vAddress, aPort));
        }

		//Stop listener in common
        public void Stop()
        {
            listening = false;
            if (listener.IsAlive)
            {
                listener.Abort();
            }
        }

		//Listen listener in common
        protected void Listen(object aIPEndPoint)
        {
            IPEndPoint vIPEndPoint = (IPEndPoint)aIPEndPoint;
            UdpClient vStreamer = new UdpClient(vIPEndPoint.Port);
            try
            {
                while (listening)
                {
                    Byte[] vBytes = vStreamer.Receive(ref vIPEndPoint);
                    if (vBytes.Length > 0)
                    {
						//KinectListener will parse the stream
                        ParseStream(vBytes);
                    }
                    else
                    {
                        Messages.LogWarning("UDP empty packet received");
                    }
                }
            }
            catch (ThreadAbortException aError)
            {
                Messages.LogWarning("Listener stopping forced: " + aError.Source);
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
            return listening && listener != null && listener.IsAlive;
        }
        
        public abstract void ParseStream(byte[] aBytes);
    }
}
