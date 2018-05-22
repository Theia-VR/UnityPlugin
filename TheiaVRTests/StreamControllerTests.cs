using Microsoft.VisualStudio.TestTools.UnitTesting;
using TheiaVR.Controllers;

namespace TheiaVRTests
{
    [TestClass]
    public class StreamControllerTests
    {
        
        [TestMethod]
        public void TestStart()
        {

            string vHost = Properties.Settings.Default.stream_host;
            int vPort = Properties.Settings.Default.stream_port;
            StreamController.GetInstance().Start(vHost, vPort);

            Assert.IsTrue(StreamController.GetInstance().IsActive());
        }

        [TestMethod]
        public void TestStop()
        {
            StreamController.GetInstance().Stop();

            Assert.IsFalse(StreamController.GetInstance().IsActive());
        }
    }
}
