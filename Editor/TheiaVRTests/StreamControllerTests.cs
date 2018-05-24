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
            StreamController.GetInstance().Start(true, false);

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
