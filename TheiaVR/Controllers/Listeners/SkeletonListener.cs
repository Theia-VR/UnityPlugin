using System;
using TheiaVR.Helpers;
using TheiaVR.Model;
using TheiaVR.Graphics;
using System.Collections.Generic;

namespace TheiaVR.Controllers.Listeners
{
    class SkeletonListener : UDPStreamListener
    {

        public override void ParseStream(byte[] aBytes)
        {

            List<Vertex> vSkeleton = new List<Vertex>(aBytes.Length - 8);

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

                    vSkeleton.Add(new Vertex(x, y, z, r, g, b));

                    vByteIndex += 16;
                }
                catch (Exception vError)
                {
                    Messages.LogError("Cannot read Vertex number " + vVertexIndex + ": " + vError.Message);
                }
                
            }
            SkeletonRenderer.GetInstance().UpdatePositions(vSkeleton);
        }
    }
}
