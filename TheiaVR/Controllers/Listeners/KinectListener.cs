using System;
using TheiaVR.Graphics;
using TheiaVR.Helpers;

namespace TheiaVR.Controllers.Listeners
{
    class KinectListener : UdpStreamListener
    {
		//Keeping the index of the parseed Item
        private readonly int byteIndex;

		// buffer to be refill when receiving points
        private FrameBuffer buffer;

        public KinectListener(FrameBuffer aBuffer, int aByteIndex)
        {
            buffer = aBuffer;
            byteIndex = aByteIndex;
        }
        
        public override void ParseStream(byte[] aBytes)
        {
			//If we haven't finish parsing
            if (aBytes.Length - byteIndex > 0)
            {
				//We receive a timeStamp first at 0 
                long vTimeStamp = BitConverter.ToInt64(aBytes, 0);

				//Keeping the first vertex index
                int vVertexIndex = 0;
				
                int vByteIndex = byteIndex;

                while (vByteIndex < aBytes.Length)
                {
                    try
                    {
						//1 bytes for each coordinates
                        float x = BitConverter.ToSingle(aBytes, vByteIndex);
                        float y = BitConverter.ToSingle(aBytes, vByteIndex + 4);
                        float z = BitConverter.ToSingle(aBytes, vByteIndex + 8);
						//1 bytes for each color
                        byte r = aBytes[vByteIndex + 12];
                        byte g = aBytes[vByteIndex + 13];
                        byte b = aBytes[vByteIndex + 14];

						//we fill the buffer with new points
                        buffer.AddPoint(vTimeStamp, x, y, z, r, g, b);

						//a new vertex has been added
                        vVertexIndex++;
						//we have parse 16bytes, so we go to the +16bytes in aBytes
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
