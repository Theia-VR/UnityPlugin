using System;
using TheiaVR.Model;
using TheiaVR.Graphics;
using NUnit.Framework;
using System.Collections.Generic;

namespace TheiaVRTests
{
    [TestFixture]
    class FrameBufferTests
    {

        [TestCase(0)]
        [TestCase(1)]
        public void TestGetLength(int aNbOfVertexs)
        {
            FrameBuffer vFrameBuffer = new FrameBuffer(aNbOfVertexs);

            Assert.AreEqual(aNbOfVertexs, vFrameBuffer.GetLength());
        }

        [Test]
        public void TestGetCount()
        {
            FrameBuffer vFrameBuffer = new FrameBuffer(0);

            Assert.AreEqual(0, vFrameBuffer.GetCount());
        }

        [Test]
        public void TestIsEmpty()
        {
            FrameBuffer vFrameBuffer = new FrameBuffer(0);

            Assert.IsTrue(vFrameBuffer.IsEmpty());
        }

        [Test]
        public void TestIsFull()
        {
            FrameBuffer vFrameBuffer = new FrameBuffer(1);

            vFrameBuffer.Queue(new Vertex(0, 0, 0, 0, 0, 0));

            Assert.IsTrue(vFrameBuffer.IsFull());
        }

        [Test]
        public void TestQueue()
        {
            FrameBuffer vFrameBuffer = new FrameBuffer(1);

            vFrameBuffer.Queue(new Vertex(0, 0, 0, 0, 0, 0));

            Assert.IsFalse(vFrameBuffer.IsEmpty());
            Assert.IsTrue(vFrameBuffer.IsFull());
            Assert.AreEqual(1, vFrameBuffer.GetCount());
            Assert.AreEqual(1, vFrameBuffer.GetLength());
            
            vFrameBuffer.Queue(new Vertex(0, 0, 0, 0, 0, 0));

            Assert.AreEqual(1, vFrameBuffer.GetCount());
        }

        [Test]
        public void TestQueueList()
        {
            List<Vertex> vVertexs = new List<Vertex>(2);

            FrameBuffer vFrameBuffer = new FrameBuffer(2);
            
            vVertexs.Add(new Vertex(0, 0, 0, 0, 0, 0));
            vVertexs.Add(new Vertex(0, 0, 0, 0, 0, 0));

            vFrameBuffer.Queue(vVertexs);

            Assert.IsFalse(vFrameBuffer.IsEmpty());
            Assert.IsTrue(vFrameBuffer.IsFull());
            Assert.AreEqual(vVertexs.Count, vFrameBuffer.GetCount());
            Assert.AreEqual(vVertexs.Count, vFrameBuffer.GetLength());

            vFrameBuffer.Queue(new Vertex(0, 0, 0, 0, 0, 0));

            Assert.AreEqual(vVertexs.Count, vFrameBuffer.GetCount());
        }

        [Test]
        public void Dequeue()
        {
            FrameBuffer vFrameBuffer = new FrameBuffer(1);

            vFrameBuffer.Queue(new Vertex(0, 0, 0, 0, 0, 0));

            vFrameBuffer.Dequeue();

            Assert.IsTrue(vFrameBuffer.IsEmpty());
            Assert.IsFalse(vFrameBuffer.IsFull());
            Assert.AreEqual(0, vFrameBuffer.GetCount());
            Assert.AreEqual(1, vFrameBuffer.GetLength());

            Assert.Throws(typeof(Exception), delegate {
                vFrameBuffer.Dequeue();
            });
        }

        [Test]
        public void DequeueNValues()
        {
            FrameBuffer vFrameBuffer = new FrameBuffer(1);

            vFrameBuffer.Queue(new Vertex(0, 0, 0, 0, 0, 0));

            vFrameBuffer.Dequeue(1);

            Assert.IsTrue(vFrameBuffer.IsEmpty());
            Assert.IsFalse(vFrameBuffer.IsFull());
            Assert.AreEqual(0, vFrameBuffer.GetCount());
            Assert.AreEqual(1, vFrameBuffer.GetLength());

            Assert.Throws(typeof(Exception), delegate {
                vFrameBuffer.Dequeue(1);
            });
        }

        [Test]
        public void DequeueAll()
        {
            FrameBuffer vFrameBuffer = new FrameBuffer(2);

            vFrameBuffer.Queue(new Vertex(0, 0, 0, 0, 0, 0));
            vFrameBuffer.Queue(new Vertex(0, 0, 0, 0, 0, 0));
            
            vFrameBuffer.DequeueAll();

            Assert.IsTrue(vFrameBuffer.IsEmpty());
            Assert.IsFalse(vFrameBuffer.IsFull());
            Assert.AreEqual(0, vFrameBuffer.GetCount());
            Assert.AreEqual(2, vFrameBuffer.GetLength());

            Assert.Throws(typeof(Exception), delegate {
                vFrameBuffer.Dequeue();
            });
        }
        
    }
}
