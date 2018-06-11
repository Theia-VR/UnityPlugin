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
        
        private Dictionary<int, GameObject> meshes;
        private Dictionary<int, KinectListener> cloudListeners;

        private GameObject skeletonObject;
        private KinectListener skeletonListener;
        
        private bool listening;

        private PluginController() {

            cloudListeners = new Dictionary<int, KinectListener>();
            meshes = new Dictionary<int, GameObject>();

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
            foreach (NetworkConfig vKinectConfig in aKinectNetworkConfigs)
            {
                Start(vKinectConfig);
            }
        }

        public void Start(NetworkConfig aNetworkConfig)
        {
            if (aNetworkConfig.EnableCloud)
            {
                if (!meshes.ContainsKey(aNetworkConfig.Id) && !cloudListeners.ContainsKey(aNetworkConfig.Id))
                {
                    GameObject vCloudMesh = Resources.Load("Cloud", typeof(GameObject)) as GameObject;
                    vCloudMesh = MonoBehaviour.Instantiate(vCloudMesh);
                    vCloudMesh.name = "Kinect - " + aNetworkConfig.Id + " - CR";
                    vCloudMesh.AddComponent<CloudRenderer>();

                    FrameBuffer vBuffer = new FrameBuffer();

                    vCloudMesh.GetComponent<CloudRenderer>().SetBuffer(vBuffer);
                    vCloudMesh.GetComponent<CloudRenderer>().SetRemanence(aNetworkConfig.Remanence);

                    meshes.Add(aNetworkConfig.Id, vCloudMesh);
                        
                    cloudListeners.Add(aNetworkConfig.Id, new KinectListener(vBuffer, 8));

                    Messages.Log("Launching cloud listener: " + aNetworkConfig.IpAddress + ", " + aNetworkConfig.CloudPort);
                    cloudListeners[aNetworkConfig.Id].Start(aNetworkConfig.IpAddress, aNetworkConfig.CloudPort);
                    Messages.Log("Cloud listener started for Kinect n°" + aNetworkConfig.Id);

                    listening = true;
                }
                else
                {
                    throw new PluginException("Conflict, a cloud mesh is already instanciated for this kinect");
                }

            }

            if (aNetworkConfig.EnableSkel)
            {
                if (skeletonObject == null && skeletonListener == null)
                {
                    skeletonObject = Resources.Load("Skeleton", typeof(GameObject)) as GameObject;
                    skeletonObject = MonoBehaviour.Instantiate(skeletonObject);
                    skeletonObject.name = "Kinect - " + aNetworkConfig.Id + " - SR";
                    skeletonObject.AddComponent<SkeletonRenderer>();

                    FrameBuffer vBuffer = new FrameBuffer();

                    skeletonObject.GetComponent<SkeletonRenderer>().SetBuffer(vBuffer);

                    skeletonListener = new KinectListener(vBuffer, 9);

                    Messages.Log("Launching skeleton listener: " + aNetworkConfig.IpAddress + ", " + aNetworkConfig.SkelPort);
                    skeletonListener.Start(aNetworkConfig.IpAddress, aNetworkConfig.SkelPort);
                    Messages.Log("Skeleton listener started for Kinect n°" + aNetworkConfig.Id);

                    listening = true;
                }
                else
                {
                    throw new PluginException("Conflict, a skeleton object is already instanciated for this kinect");
                }
            }
        }

        public void Stop()
        {

            foreach (KeyValuePair<int, GameObject> vCloudInfos in meshes)
            {
                if (cloudListeners.ContainsKey(vCloudInfos.Key))
                {
                    cloudListeners[vCloudInfos.Key].Stop();
                    cloudListeners.Remove(vCloudInfos.Key);
                }
                MonoBehaviour.DestroyImmediate(vCloudInfos.Value);

                Messages.Log("Cloud treatment stopped for Kinect n°" + vCloudInfos.Key);
            }

            meshes.Clear();
            
            if (skeletonListener != null && skeletonObject != null)
            {
                skeletonListener.Stop();
                MonoBehaviour.DestroyImmediate(skeletonObject);

                skeletonListener = null;
                skeletonObject = null;
                Messages.Log("Skeleton treatment stopped");

            }

            listening = false;
        }

        public bool IsActive()
        {
            return listening;
        }
    }
}
