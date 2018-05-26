using System.Collections.Generic;

namespace TheiaVR.Model
{
    class Skeleton
    {
        private byte tag;
        
        private List<Vertex> joints;

        public Skeleton(byte aTag)
        {
            joints = new List<Vertex>();
            tag = aTag;
        }
        
        public byte getTag()
        {
            return tag;
        }

        public void AddJoint(Vertex aJoint)
        {
            lock (joints)
            {
                joints.Add(aJoint);
            }
        }

        public List<Vertex> GetJoints()
        {
            return joints;
        }
        
    }
}
