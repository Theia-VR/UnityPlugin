using TheiaVR.Helpers;
using TheiaVR.Graphics;
using TheiaVR.Controllers.Listeners;
using UnityEditor;

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
        
        public void Start(string aAddress, int aSkeletonPort, int aCloudPort, bool aStartSkeleton, bool aStartCloud)
        {
            if (skeleton == null && aStartSkeleton)
            {
                if (SkeletonRenderer.GetInstance() != null)
                {
                    FrameBuffer vBuffer = new FrameBuffer(25);
                    Messages.Log("Skeleton buffer initialized");

                    SkeletonRenderer.GetInstance().SetBuffer(vBuffer);
                    Messages.Log("Skeleton buffer added to Skeleton renderer");

                    skeleton = new KinectListener(vBuffer, 9);
                    Messages.Log("SkeletonListener initialized");

                    skeleton.Start(aAddress, aSkeletonPort);
                    Messages.Log("SkeletonListener started");
                }
                else
                {
                    Messages.LogError("SkeletonRenderer is not activated. Please associate it to a game component and try again.");
                }
            }

            if (cloud == null && aStartCloud)
            {
                if(CloudRenderer.GetInstance() != null)
                {
                    FrameBuffer vBuffer = new FrameBuffer(3000);//EditorPrefs.GetInt("pointAmount"));
                    Messages.Log("Cloud buffer initialized");

                    CloudRenderer.GetInstance().SetBuffer(vBuffer);
                    Messages.Log("Cloud buffer added to Cloud renderer");

                    cloud = new KinectListener(vBuffer, 8);
                    Messages.Log("CloudListener initialized");

                    cloud.Start(aAddress, aCloudPort);
                    Messages.Log("CloudListener started");
                }
                else
                {
                    Messages.LogError("CloudRenderer is not activated. Please associate it to a game component and try again.");
                }
            }
        }

        public void Stop()
        {
            if (skeleton != null && skeleton.IsActive())
            {
                skeleton.Stop();
                skeleton = null;
                Messages.Log("Skeleton listener stopped");
            }

            if (cloud != null && cloud.IsActive())
            {
                cloud.Stop();
                cloud = null;
                Messages.Log("Cloud listener stopped");
            }
        }

        public bool IsActive()
        {
            return (skeleton != null && skeleton.IsActive()) || (cloud != null && cloud.IsActive());
        }
    }
}
