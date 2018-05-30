using NUnit.Framework;
using TheiaVR.Controllers;

namespace TheiaVRTests
{
    [TestFixture]
    class StreamControllerTests
    {
        [Test]
        public void TestStart()
        {
            StreamController.GetInstance().Start("127.0.0.1", 9877, 9776, true, false);

            Assert.IsTrue(StreamController.GetInstance().IsActive());
        }

        [Test]
        public void TestStop()
        {
            StreamController.GetInstance().Stop();

            Assert.IsFalse(StreamController.GetInstance().IsActive());
        }
    }
}
