using System;
using System.Collections.Generic;

using TheiaVR.Model;
using TheiaVR.Graphics;
using TheiaVR.Helpers;
using TheiaVR.Helpers.Observer;
using TheiaVR.Controllers.Listeners;

using UnityEngine;

namespace TheiaVR.Controllers
{
    public class RenderingController : Observer 
    {
        private NetworkConfig config;
        
        private KinectListener listener;

        private List<KinectRenderer> renderers;

        private Type rendererType;

        public RenderingController(NetworkConfig aNetworkConfig, Type aRendererClass, int aByteIndex)
        {
            config = aNetworkConfig;

            rendererType = aRendererClass;

            listener = new KinectListener(aByteIndex);
            listener.Attach(this);

            renderers = new List<KinectRenderer>();
        }

        public void CreateRenderers()
        {
            if (rendererType == typeof(CloudRenderer))
            {
                for (int i = 0; i <= config.Remanence; i++)
                {
                    GameObject vCloudMesh = Resources.Load("Cloud", typeof(GameObject)) as GameObject;
                    vCloudMesh = MonoBehaviour.Instantiate(vCloudMesh);
                    vCloudMesh.name = "Kinect - " + config.Id + "." + i + " - CR";
                    vCloudMesh.AddComponent<CloudRenderer>();

                    renderers.Add(vCloudMesh.GetComponent<CloudRenderer>());
                }
            }
            else if(rendererType == typeof(SkeletonRenderer))
            {
                GameObject vSkeletonObject = Resources.Load("Skeleton", typeof(GameObject)) as GameObject;
                vSkeletonObject = MonoBehaviour.Instantiate(vSkeletonObject);
                vSkeletonObject.name = "Kinect - " + config.Id + " - SR";
                vSkeletonObject.AddComponent<SkeletonRenderer>();

                renderers.Add(vSkeletonObject.GetComponent<SkeletonRenderer>());
            }
            else
            {
                throw new PluginException("Renderer specified are unknown. Use CloudRenderer or SkeletonRenderer");
            }
        }

        public void Start()
        {
            int vPort = 0;

            if (rendererType == typeof(CloudRenderer))
            {
                vPort = config.CloudPort;
            }
            else if (rendererType == typeof(SkeletonRenderer))
            {
                vPort = config.SkelPort;
            }
            else
            {
                throw new PluginException("Renderer specified are unknown. Use CloudRenderer or SkeletonRenderer");
            }

            Messages.Log("Launching cloud listener: " + config.IpAddress + ", " + vPort);
            listener.Start(config.IpAddress, vPort);
            Messages.Log("Cloud listener started for Kinect n°" + config.Id);
        }

        public void Stop()
        {
            listener.Stop();
            Messages.Log("Cloud listener stopped for Kinect n°" + config.Id);
        }

        public void DestroyRenderers()
        {
            foreach (KinectRenderer vRenderer in renderers)
            {
                MonoBehaviour.DestroyImmediate(vRenderer.gameObject);
            }

            renderers.Clear();
        }

        public void Update(object aObject)
        {
            Frame vFrame = (Frame)aObject;
            if(vFrame != null && renderers.Count > 0)
            {
                KinectRenderer vRenderer = renderers[0];

                if (renderers.Count > 1)
                {
                    renderers.RemoveAt(0);
                    renderers.Add(vRenderer);
                }

                vRenderer.SetFrame(vFrame);
            }
        }
    }
}
