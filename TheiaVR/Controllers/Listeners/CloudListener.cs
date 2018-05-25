using System;
using TheiaVR.Helpers;
using TheiaVR.Model;

namespace TheiaVR.Controllers.Listeners
{
    class CloudListener : UDPStreamListener
    {

        private long timestamp;

        public CloudListener()
        {
            timestamp = 0;
        }

        public override void ParseStream(byte[] aBytes)
        {

            Cloud vCloud = new Cloud();

            bool vNewFrame = false;

            Vertex[] vFrame = null;

            long vTimeStamp = BitConverter.ToInt64(aBytes, 0);
            if (vTimeStamp > timestamp)
            {
                timestamp = vTimeStamp;
                vNewFrame = true;
                if (vFrame != null)
                {
                    vCloud.AddFrame(vFrame);
                }
            }

            if (aBytes.Length - 8 > 0)
            {
                Vertex[] vVertex = new Vertex[(aBytes.Length - 8) / 16];
                int vVertexIndex = 0;
                int vByteIndex = 9;
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

                        vFrame[vFrame.Length - 1] = new Vertex(x, y, z, r, g, b);

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
                    Vertex[] vResult = new Vertex[vFrame.Length + vVertex.Length];
                    vFrame.CopyTo(vResult, 0);

                    Array.Copy(vVertex, 0, vResult, vFrame.Length, vVertex.Length);
                    vFrame = vResult;
                }

            }
            
        }
    }
}
