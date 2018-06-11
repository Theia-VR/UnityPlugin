using TheiaVR.Graphics;
using TheiaVR.Helpers;
using NUnit.Framework;

namespace TheiaVRTests
{
    [TestFixture]
    class FrameBufferTests
    {
        [Test]
        public void TestIsEmpty()
        {
            FrameBuffer vBuffer = new FrameBuffer();

            Assert.IsTrue(vBuffer.IsEmpty());
        }

        [Test]
        public void TestAddPoint()
        {
            FrameBuffer vBuffer = new FrameBuffer();

            vBuffer.AddPoint(0, 0, 0, 0, 0, 0, 0);
            vBuffer.AddPoint(1, 0, 0, 0, 0, 0, 0);

            Assert.IsFalse(vBuffer.IsEmpty());
        }

        [Test]
        public void Dequeue()
        {
            FrameBuffer vBuffer = new FrameBuffer();

            vBuffer.AddPoint(0, 0, 0, 0, 0, 0, 0);
            vBuffer.AddPoint(1, 0, 0, 0, 0, 0, 0);

            vBuffer.Dequeue();

            Assert.IsTrue(vBuffer.IsEmpty());

            Assert.Throws(typeof(PluginException), delegate {
                vBuffer.Dequeue();
            });
        }
    }
}
