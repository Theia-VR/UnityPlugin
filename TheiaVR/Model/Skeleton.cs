namespace TheiaVR.Model
{
    class Skeleton
    {
        private byte tag;
        
        private Vertex[] joints;

        public Skeleton(byte aTag, Vertex[] aJoints)
        {
            joints = aJoints;
            tag = aTag;
        }

        public Skeleton(byte aTag, int aNumberOfJoints) : this(aTag, new Vertex[aNumberOfJoints]) {}

        public byte getTag()
        {
            return tag;
        }

        public void AddJoint(Vertex aJoint)
        {
            joints[joints.Length-1] = aJoint;
        }

        public Vertex[] GetJoints()
        {
            return joints;
        }
        
    }
}
