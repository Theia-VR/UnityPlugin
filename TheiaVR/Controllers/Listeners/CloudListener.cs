using System;
using TheiaVR.Helpers;
using TheiaVR.Model;
using System.Collections;
using System.Collections.Generic;
using TheiaVR.Graphics;

namespace TheiaVR.Controllers.Listeners
{
    class CloudListener : UDPStreamListener
    {

        private long timestamp;
        private List<Vertex> cloud;

        public CloudListener()
        {
            timestamp = 0;
            cloud = new List<Vertex>(3000);
        }

        public override void ParseStream(byte[] aBytes)
        {

            List<Vertex> vFrame = new List<Vertex>(aBytes.Length - 8);

            bool vNewFrame = false;
            
            long vTimeStamp = BitConverter.ToInt64(aBytes, 0);
            if (vTimeStamp > timestamp)
            {
                timestamp = vTimeStamp;
                vNewFrame = true;
                if (vFrame != null && vFrame.Count > 0)
                {
                    cloud.AddRange(vFrame);
                }
            }

            if (aBytes.Length - 8 > 0)
            {
                List<Vertex> vVertex = new List<Vertex>((aBytes.Length - 8) / 16);
                int vVertexIndex = 0;
                int vByteIndex = 8;
                while (vByteIndex < aBytes.Length)
                {
                    try
                    {
                        float x = BitConverter.ToSingle(aBytes, vByteIndex);
                        float y = BitConverter.ToSingle(aBytes, vByteIndex + 4);
                        float z = BitConverter.ToSingle(aBytes, vByteIndex + 8);

                        byte r = aBytes[vByteIndex + 12];
                        byte g = aBytes[vByteIndex + 13];
                        byte b = aBytes[vByteIndex + 14];
                        
                        vFrame.Add(new Vertex(x, y, z, r, g, b));
                        
                        vVertexIndex++;
                        vByteIndex += 16;
                    }
                    catch (Exception vError)
                    {
                        Messages.LogError("Cannot read Vertex number " + vVertexIndex + ": " + vError.Message);
                    }

                }
                if (vNewFrame)
                {
                    vFrame = vVertex;
                }
                else
                {
                    vFrame.AddRange(vVertex);
                }

            }
            for(int i = 0; i < cloud.Count; i++)
            {
                Messages.Log(cloud[i].ToString());
            }
            CloudRenderer.GetInstance().UpdatePositions(cloud);
        }
    }
}
