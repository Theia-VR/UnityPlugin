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
            string vHost = Properties.Settings.Default.stream_host;
            int vPort = Properties.Settings.Default.stream_port;
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
