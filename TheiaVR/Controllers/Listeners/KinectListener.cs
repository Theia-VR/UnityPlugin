using System;
using TheiaVR.Model;
using TheiaVR.Helpers;

namespace TheiaVR.Controllers.Listeners
{
    class KinectListener : UdpStreamListener
    {
		// UDP packet head size (skeleton -> 9, cloud -> 8)
        private readonly int byteIndex;
        
        // Current frame in build
        private Frame frame;

        // Current timestamp of the frame we are building
        private long timestamp;

        public KinectListener(int aByteIndex)
        {
            byteIndex = aByteIndex;
        }
        
        public override void ParseStream(byte[] aBytes)
        {
			//If we haven't finish parsing
            if (aBytes.Length - byteIndex > 0)
            {
				// Frame timeStamp is the first byte of the chunk 
                long vTimeStamp = BitConverter.ToInt64(aBytes, 0);

				//Keeping the first vertex index
                int vVertexIndex = 0;
				
                int vByteIndex = byteIndex;

                while (vByteIndex < aBytes.Length)
                {
                    try
                    {
						// 4 bytes for each coordinates
                        float x = BitConverter.ToSingle(aBytes, vByteIndex);
                        float y = BitConverter.ToSingle(aBytes, vByteIndex + 4);
                        float z = BitConverter.ToSingle(aBytes, vByteIndex + 8);
						// 1 bytes for each color
                        byte r = aBytes[vByteIndex + 12];
                        byte g = aBytes[vByteIndex + 13];
                        byte b = aBytes[vByteIndex + 14];

                        BuildFrame(vTimeStamp, x, y, z, r, g, b);

						// a new vertex has been added
                        vVertexIndex++;
						// we have parse 16bytes, so we go to the +16bytes in aBytes
                        vByteIndex += 16;
                    }
                    catch (Exception vError)
                    {
                        Messages.LogError("Cannot read Vertex number " + vVertexIndex + ": " + vError.Message);
                    }
                }
            }
        }

        public void BuildFrame(long aTimeStamp, float aX, float aY, float aZ, byte aR, byte aG, byte aB)
        {
            if (aTimeStamp > timestamp)
            {
                timestamp = aTimeStamp;
                Notify(frame);

                frame = new Frame();
            }
            frame.AddPoint(aX, aY, aZ, aR, aG, aB);            
        }
    }
}
