using System;
using System.Collections.Generic;
using TheiaVR.Graphics;
using TheiaVR.Model;
using TheiaVR.Helpers;

namespace TheiaVR.Controllers.Listeners
{
    class KinectListener : UDPStreamListener
    {
        private long timestamp;
        private int byteIndex;
        private FrameBuffer buffer;

        public KinectListener(FrameBuffer aBuffer, int aByteIndex)
        {
            timestamp = 0;
            buffer = aBuffer;
            byteIndex = aByteIndex;
        }
        
        public override void ParseStream(byte[] aBytes)
        {

            long vTimeStamp = BitConverter.ToInt64(aBytes, 0);
            if (vTimeStamp > timestamp)
            {
                if (aBytes.Length > 0)
                {
                    int vVertexIndex = 0;
                    int vByteIndex = byteIndex;
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

                            buffer.Queue(new Vertex(x, y, z, r, g, b));

                            vVertexIndex++;
                            vByteIndex += 16;
                        }
                        catch (Exception vError)
                        {
                            Messages.LogError("Cannot read Vertex number " + vVertexIndex + ": " + vError.Message);
                        }
                    }
                }
            }
        }
    }
}
