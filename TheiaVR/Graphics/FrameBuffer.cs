using System;
using System.Collections.Generic;
using TheiaVR.Model;

namespace TheiaVR.Graphics
{
    public class FrameBuffer
    {
        private long timestamp;

        private Frame frame;

        private List<Frame> frames;

        public FrameBuffer()
        {
            timestamp = 0;
            frame = new Frame();
            frames = new List<Frame>();
        }

        public bool IsEmpty()
        {
            return frames.Count <= 0;
        }

        public void AddPoint(long aTimeStamp, float aX, float aY, float aZ, byte aR, byte aG, byte aB)
        {
            if (aTimeStamp > timestamp)
            {
                timestamp = aTimeStamp;
                frames.Add(frame);
                frame = new Frame();
            }

            frame.AddPoint(aX, aY, aZ, aR, aG, aB);
        }


        public Frame Dequeue()
        {
            if (IsEmpty())
            {
                throw new Exception("Frame buffer is empty, cannot dequeue");
            }

            Frame vFrame = frames[0];
            frames.RemoveAt(0);
            return vFrame;
        }
    }
}
