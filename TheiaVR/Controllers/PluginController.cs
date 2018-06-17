using System.Collections.Generic;
using TheiaVR.Model;
using TheiaVR.Graphics;

namespace TheiaVR.Controllers
{
    public class PluginController
    {
        private static PluginController instance = null;
        
        private List<RenderingController> cloudRenderers;
        
        private RenderingController skeletonRenderer;
       
		//to check if the listeners correctly started
        private bool listening;

        private PluginController() {

            cloudRenderers = new List<RenderingController>();
            skeletonRenderer = null;
            listening = false;
        }
        
        public static PluginController GetInstance()
        {
            if (instance == null)
            {
                instance = new PluginController();
            }
            return instance;
        }
        
        public void Start(List<NetworkConfig> aKinectNetworkConfigs)
        {
			// We start each Kinect dataFlow with configs given by user
            foreach (NetworkConfig vKinectConfig in aKinectNetworkConfigs)
            {
                Start(vKinectConfig);
            }
        }

        public void Start(NetworkConfig aNetworkConfig)
        {
			//Cloud instanciation
            if (aNetworkConfig.EnableCloud)
            {
                RenderingController vCloudRenderer = new RenderingController(aNetworkConfig, typeof(CloudRenderer), 8);
                vCloudRenderer.CreateRenderers();
                vCloudRenderer.Start();
                cloudRenderers.Add(vCloudRenderer);

                listening = true;
            }

            if (aNetworkConfig.EnableSkel && skeletonRenderer == null)
            {
                RenderingController vSkeletonRenderer = new RenderingController(aNetworkConfig, typeof(SkeletonRenderer), 9);
                vSkeletonRenderer.CreateRenderers();
                vSkeletonRenderer.Start();
                skeletonRenderer = vSkeletonRenderer;

                listening = true;
            }
        }

        public void Stop()
        {
            
            foreach (RenderingController vCloudRenderer in cloudRenderers)
            {
                vCloudRenderer.Stop();
                vCloudRenderer.DestroyRenderers();
            }

            cloudRenderers.Clear();
            
            if (skeletonRenderer != null )
            {
                skeletonRenderer.Stop();
                skeletonRenderer.DestroyRenderers();

                skeletonRenderer = null;
            }

			//We don't listen anymore
            listening = false;
        }

		//For other classes to know if we are launched
        public bool IsActive()
        {
            return listening;
        }
    }
}
