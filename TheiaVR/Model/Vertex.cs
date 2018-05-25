namespace TheiaVR.Model
{
    class Vertex
    {

        private float x { get; }
        private float y { get; }
        private float z { get; }
        private byte r { get; }
        private byte g { get; }
        private byte b { get; }
        private byte tag { get; }

        public Vertex(float aX, float aY, float aZ, byte aR, byte aG, byte aB)
        {
            x = aX;
            y = aY;
            z = aZ;
            r = aR;
            g = aG;
            b = aB;
        }

        public Vertex(float aX, float aY, float aZ, byte aR, byte aG, byte aB, byte aTag) : this(aX, aY, aZ, aR, aG, aB)
        {
            tag = aTag;
        }

        public override string ToString()
        {
            return "Vertex[x: " + x + ", y:" + y + ", z:" + z + ", r:" + r + ", g:" + g + ",b:" + b + ",tag:" + tag + "]";
        }

    }

}
