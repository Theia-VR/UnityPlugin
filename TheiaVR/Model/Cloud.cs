using System.Collections;

namespace TheiaVR.Model
{
    class Cloud
    {
        private byte tag;
        
        private ArrayList frames;

        public Cloud()
        {
            frames = new ArrayList();
        }

        public Cloud(byte aTag)
        {
            tag = aTag;
        }
        
        public byte getTag()
        {
            return tag;
        }
        
        public void AddFrame(Vertex[] aFrame)
        {
            frames.Add(aFrame);
        }

        public ArrayList GetFrames()
        {
            return frames;
        }
    }
}
