using System.Collections.Generic;
using TheiaVR.Model;
using TheiaVR.Controllers.Listeners;
using TheiaVR.Graphics;
using UnityEngine;
using TheiaVR.Helpers;
using System;

namespace TheiaVR.Controllers
{
    public class PluginController
    {
        private static PluginController instance = null;

        private Dictionary<int, NetworkConfig> kinectConfigs;

        private Dictionary<int, KinectListener> listeners;

        private KinectListener skeletonListener;
        
        private Dictionary<int, CloudRenderer> cloudRenderers;

        private Dictionary<int, SkeletonRenderer> skeletonRenderers;

        private bool listening;

        private PluginController() {

            kinectConfigs = new Dictionary<int, NetworkConfig>();
            listeners = new Dictionary<int, KinectListener>();
            cloudRenderers = new Dictionary<int, CloudRenderer>();
            skeletonRenderers = new Dictionary<int, SkeletonRenderer>();

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
        
        public void SetKinectConfiguration(NetworkConfig aKinectNetworkConfig)
        {
            if (kinectConfigs.ContainsKey(aKinectNetworkConfig.Id))
            {
                kinectConfigs[aKinectNetworkConfig.Id] = aKinectNetworkConfig;
            }
            else
            {
                kinectConfigs.Add(aKinectNetworkConfig.Id, aKinectNetworkConfig);
            }
        }

        public void SetKinectConfigurations(List<NetworkConfig> aKinectNetworkConfigs)
        {
            foreach (NetworkConfig vKinectConfig in aKinectNetworkConfigs)
            {
                SetKinectConfiguration(vKinectConfig);
            }
        }

        public void Start()
        {
            foreach (NetworkConfig vConfig in kinectConfigs.Values)
            {
                if (vConfig.EnableCloud)
                {
                    if (GameObject.Find("Kinect - " + vConfig.Id + " - CR") == null)
                    {
                        GameObject vCloudMesh = Resources.Load("Cloud", typeof(GameObject)) as GameObject;
                        vCloudMesh = MonoBehaviour.Instantiate(vCloudMesh);
                        vCloudMesh.name = "Kinect - " + vConfig.Id + " - CR";
                        vCloudMesh.AddComponent<CloudRenderer>();

                        FrameBuffer vBuffer = new FrameBuffer();
                        Messages.Log("IP: " + vConfig.IpAddress);

                        cloudRenderers.Add(vConfig.Id, vCloudMesh.GetComponent<CloudRenderer>() as CloudRenderer);
                        cloudRenderers[vConfig.Id].SetBuffer(vBuffer);
                        
                        listeners.Add(vConfig.Id, new KinectListener(vBuffer, 8));

                        listeners[vConfig.Id].Start(vConfig.IpAddress, vConfig.CloudPort);

                        listening = true;
                    }
                    else
                    {
                        throw new Exception("Conflict, a cloud mesh is already instanciated for this kinect");
                    }

                }

                if (vConfig.EnableSkel && skeletonListener == null)
                {
                    if (GameObject.Find("Kinect - " + vConfig.Id + " - SR") == null)
                    {
                        GameObject vSkeleton = Resources.Load("Skeleton", typeof(GameObject)) as GameObject;
                        vSkeleton = MonoBehaviour.Instantiate(vSkeleton);
                        vSkeleton.name = "Kinect - " + vConfig.Id + " - SR";
                        vSkeleton.AddComponent<SkeletonRenderer>();

                        FrameBuffer vBuffer = new FrameBuffer();

                        skeletonRenderers.Add(vConfig.Id, vSkeleton.GetComponent<SkeletonRenderer>());
                        skeletonRenderers[vConfig.Id].SetBuffer(vBuffer);

                        skeletonListener = new KinectListener(vBuffer, 9);

                        skeletonListener.Start(vConfig.IpAddress, vConfig.SkelPort);

                        listening = true;
                    }
                    else
                    {
                        throw new Exception("Conflict, a skeleton object is already instanciated for this kinect");
                    }
                }
            }
        }

        public void Stop()
        {
            foreach (NetworkConfig vConfig in kinectConfigs.Values)
            {
                if (listeners.ContainsKey(vConfig.Id))
                {
                    listeners[vConfig.Id].Stop();

                    if (cloudRenderers.ContainsKey(vConfig.Id))
                    {
                        MonoBehaviour.DestroyImmediate(cloudRenderers[vConfig.Id]);
                    }

                    if (skeletonRenderers.ContainsKey(vConfig.Id))
                    {
                        MonoBehaviour.DestroyImmediate(skeletonRenderers[vConfig.Id]);
                        skeletonListener = null;
                    }
                }
            }
            listening = false;
        }

        public bool IsActive()
        {
            return listening;
        }
    }
}
