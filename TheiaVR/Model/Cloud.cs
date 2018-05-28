using System.Collections;

namespace TheiaVR.Model
{
    class Cloud
    {
        private byte tag;
        
        private Vertex[][] frames;

        public Cloud(int aNbOfFrames)
        {
            frames = new Vertex[aNbOfFrames][];
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
            lock (frames)
            {
                frames[frames.Length - 1] =  aFrame;
            }
        }

        public Vertex[][] GetFrames()
        {
            return frames;
        }
    }
}
