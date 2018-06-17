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
        
		//meshes that we render
        private Dictionary<int, GameObject> meshes;
		
		//our listeners
        private Dictionary<int, KinectListener> cloudListeners;

		//Same for skeleton, only one instance
        private GameObject skeletonObject;
        private KinectListener skeletonListener;
        
		//to check if the listeners correctly started
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
				//Checking if we don't have this cloud 
                if (!meshes.ContainsKey(aNetworkConfig.Id) && !cloudListeners.ContainsKey(aNetworkConfig.Id))
                {
					//We load our Mesh
                    GameObject vCloudMesh = Resources.Load("Cloud", typeof(GameObject)) as GameObject;
                    vCloudMesh = MonoBehaviour.Instantiate(vCloudMesh);
                    vCloudMesh.name = "Kinect - " + aNetworkConfig.Id + " - CR";
                    vCloudMesh.AddComponent<CloudRenderer>();

					//Associate a buffer
                    FrameBuffer vBuffer = new FrameBuffer();

                    vCloudMesh.GetComponent<CloudRenderer>().SetBuffer(vBuffer);
                    vCloudMesh.GetComponent<CloudRenderer>().SetRemanence(aNetworkConfig.Remanence);

					//Adding this mesh at our dictonnary
                    meshes.Add(aNetworkConfig.Id, vCloudMesh);
                        
					//Starting a cloudListner and adding at our dictonnary
                    cloudListeners.Add(aNetworkConfig.Id, new KinectListener(vBuffer, 8));

                    Messages.Log("Launching cloud listener: " + aNetworkConfig.IpAddress + ", " + aNetworkConfig.CloudPort);
                    cloudListeners[aNetworkConfig.Id].Start(aNetworkConfig.IpAddress, aNetworkConfig.CloudPort);
                    Messages.Log("Cloud listener started for Kinect n°" + aNetworkConfig.Id);

					//We are now listening
                    listening = true;
                }
                else
                {
                    throw new PluginException("Conflict, a cloud mesh is already instanciated for this kinect");
                }

            }

            if (aNetworkConfig.EnableSkel)
            {
				//Checking if we don't have any skeleton instanciated
                if (skeletonObject == null && skeletonListener == null)
                {
					//We load our Skeleton
                    skeletonObject = Resources.Load("Skeleton", typeof(GameObject)) as GameObject;
                    skeletonObject = MonoBehaviour.Instantiate(skeletonObject);
                    skeletonObject.name = "Kinect - " + aNetworkConfig.Id + " - SR";
                    skeletonObject.AddComponent<SkeletonRenderer>();

					//Associate a buffer
                    FrameBuffer vBuffer = new FrameBuffer();

					//Adding this mesh at our single instance of Skeleton
                    skeletonObject.GetComponent<SkeletonRenderer>().SetBuffer(vBuffer);

                    skeletonListener = new KinectListener(vBuffer, 9);

                    Messages.Log("Launching skeleton listener: " + aNetworkConfig.IpAddress + ", " + aNetworkConfig.SkelPort);
                    skeletonListener.Start(aNetworkConfig.IpAddress, aNetworkConfig.SkelPort);
                    Messages.Log("Skeleton listener started for Kinect n°" + aNetworkConfig.Id);

					//We are now listening
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

		    //Stop listeners first, then meshes an skeletonObject
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
