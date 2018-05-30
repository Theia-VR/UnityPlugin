using UnityEngine;

namespace TheiaVR.Model
{
    public class Vertex
    {

        private float x { get; }
        private float y { get; }
        private float z { get; }
        private byte r { get; }
        private byte g { get; }
        private byte b { get; }

        public Vertex(float aX, float aY, float aZ, byte aR, byte aG, byte aB)
        {
            x = aX;
            y = aY;
            z = aZ;
            r = aR;
            g = aG;
            b = aB;
        }

        public Vector3 GetVector()
        {
            return new Vector3(x, y, z);
        }

        public Color GetColor()
        {
            return new Color(r / 255f, g / 255f, b / 255f);
        }

        public override string ToString()
        {
            return "Vertex[x: " + x + ", y:" + y + ", z:" + z + ", r:" + r + ", g:" + g + ",b:" + b + "]";
        }

    }

}
