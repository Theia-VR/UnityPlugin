using TheiaVR.Controllers.Listeners;

namespace TheiaVR.Controllers
{
    /**
     * Class allowing to listen UDP stream from the Kinect streamer
     */
    public class StreamController
    {
        private static StreamController instance = null;

        private UDPStreamListener skeleton;

        private UDPStreamListener cloud;

        private StreamController()
        {
            // Used to prevent public access
        }

        public static StreamController GetInstance()
        {
            if(instance == null)
            {
                instance = new StreamController();
            }
            return instance;
        }
        
        public void Start(bool aStartSkeleton, bool aStartCloud)
        {
            if (skeleton == null && aStartSkeleton)
            {
                skeleton = new SkeletonListener();
                skeleton.Start("127.0.0.1", 9877); // TODO: Replace by configuration value
            }

            if (cloud == null && aStartCloud)
            {
                cloud = new CloudListener();
                cloud.Start("127.0.0.1", 9876); // TODO: Replace by configuration value
            }
        }

        public void Stop()
        {
            if (skeleton.IsActive())
            {
                skeleton.Stop();
                skeleton = null;
            }

            if (cloud.IsActive())
            {
                cloud.Stop();
                cloud = null;
            }
        }

        public bool IsActive()
        {
            return (skeleton != null && skeleton.IsActive()) || (cloud != null && cloud.IsActive());
        }
    }
}
