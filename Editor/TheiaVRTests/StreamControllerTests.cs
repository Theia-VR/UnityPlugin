using NUnit.Framework;
using TheiaVR.Controllers;

namespace TheiaVRTests
{
    [TestFixture]
    public class StreamControllerTests
    {
        [Test]
        public void TestStart()
        {
            string vHost = "127.0.0.1";
            int vPort = 11000;
            StreamController.GetInstance().Start(vHost, vPort);

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
