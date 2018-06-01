using NUnit.Framework;
using TheiaVR.Model;

namespace TheiaVRTests
{
    [TestFixture]
    public class FrameTests
    {

        [Test]
        public void TestGetPointsNumber()
        {
            Frame vFrame = new Frame();

            Assert.AreEqual(0, vFrame.GetPointsNumber());
        }

        [Test]
        public void TestAddPoint()
        {
            Frame vFrame = new Frame();

            vFrame.AddPoint(0, 0, 0, 0, 0, 0);

            Assert.AreEqual(1, vFrame.GetPointsNumber());
        }
        
    }
}
