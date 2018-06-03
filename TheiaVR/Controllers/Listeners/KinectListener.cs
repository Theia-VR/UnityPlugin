using System;
using TheiaVR.Graphics;
using TheiaVR.Helpers;

namespace TheiaVR.Controllers.Listeners
{
    class KinectListener : UDPStreamListener
    {
        private int byteIndex;

        private FrameBuffer buffer;

        public KinectListener(FrameBuffer aBuffer, int aByteIndex)
        {
            buffer = aBuffer;
            byteIndex = aByteIndex;
        }
        
        public override void ParseStream(byte[] aBytes)
        {
            if (aBytes.Length - byteIndex > 0)
            {
                long vTimeStamp = BitConverter.ToInt64(aBytes, 0);

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

                        buffer.AddPoint(vTimeStamp, x, y, z, r, g, b);

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
