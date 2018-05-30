using System;
using TheiaVR.Controllers;
using TheiaVR.Graphics;
using TheiaVR.Helpers;
using UnityEngine;
using UnityEditor;

namespace TheiaVR.Editor
{
    public class KinectPluginWindow : EditorWindow
    {
        string ip;
        int cloudPort;
        int skelPort;
        bool enablePointCloud;
        bool enableSkeleton;
        private bool enableUnityLogs;
        private bool started;
        private bool stopped;

        // Add menu item named "Kinect Plugin" to the Window menu
        [MenuItem("Kinect Plugin/Show plugin")]
        public static void ShowWindow()
        {
            GetWindow(typeof(KinectPluginWindow), false, "Kinect Plugin");
        }

        private void OnEnable()
        {
            ip = EditorPrefs.GetString("ip", "127.0.0.1");
            cloudPort = EditorPrefs.GetInt("cloudPort", 9876);
            skelPort = EditorPrefs.GetInt("skelPort", 9877);
            enablePointCloud = EditorPrefs.GetBool("enablePointCloud", true);
            enableSkeleton = EditorPrefs.GetBool("enableSkeleton", true);
            enableUnityLogs = EditorPrefs.GetBool("enableUnityLogs", true);

            if (enablePointCloud)
            {
                AddCloudRenderer();
            }

            if (enableSkeleton)
            {
                AddSkeletonRenderer();
            }
        }

        private void OnDisable()
        {
            EditorPrefs.SetString("ip", ip);
            EditorPrefs.SetInt("cloudPort", cloudPort);
            EditorPrefs.SetInt("skelPort", skelPort);
            EditorPrefs.SetBool("enablePointCloud", enablePointCloud);
            EditorPrefs.SetBool("enableSkeleton", enableSkeleton);
            EditorPrefs.SetBool("enableUnityLogs", enableUnityLogs);
        }

        private void Awake()
        {
            if (StreamController.GetInstance().IsActive())
            {
                DisplayStopUI();
                Messages.Log("Displaying stop");
            }
            else
            {
                DisplayStartUI();
                Messages.Log("Displaying start");
            }
        }

        private void AddCloudRenderer()
        {
            if (Camera.main.gameObject.GetComponent<CloudRenderer>() == null)
            {
                Camera.main.gameObject.AddComponent<CloudRenderer>();
            }
        }

        private void AddSkeletonRenderer()
        {
            if (Camera.main.gameObject.GetComponent<SkeletonRenderer>() == null)
            {
                Camera.main.gameObject.AddComponent<SkeletonRenderer>();
            }
        }

        private void DisplayStartUI()
        {
            stopped = true;
            started = false;
        }

        private void DisplayStopUI()
        {
            stopped = false;
            started = true;
        }

        private void CheckPlayModeState(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.ExitingPlayMode)
            {
                DisplayStartUI();
            }
        }

        void OnGUI()
        {
            // Listening to play button
            EditorApplication.playModeStateChanged += CheckPlayModeState;

            GUILayout.Label("Network settings", EditorStyles.boldLabel);
            ip = EditorGUILayout.TextField("IP Address", ip);

            cloudPort = EditorGUILayout.IntField("Cloud port", cloudPort);
            skelPort = EditorGUILayout.IntField("Skeleton port", skelPort);

            GUILayout.Label("Receiving", EditorStyles.boldLabel);
            if (EditorGUILayout.Toggle("Cloud points", enablePointCloud) != enablePointCloud)
            {
                enablePointCloud = !enablePointCloud;
                if (enablePointCloud)
                {
                    AddCloudRenderer();
                }
                else
                {
                    DestroyImmediate(Camera.main.gameObject.GetComponent<CloudRenderer>());
                }
            }

            if (EditorGUILayout.Toggle("Skeleton", enableSkeleton) != enableSkeleton)
            {
                enableSkeleton = !enableSkeleton;
                if (enableSkeleton)
                {
                    AddSkeletonRenderer();
                }
                else
                {
                    DestroyImmediate(Camera.main.gameObject.GetComponent<SkeletonRenderer>());
                }
            }

            GUILayout.Label("Logs", EditorStyles.boldLabel);
            if (EditorGUILayout.Toggle("Display Unity logs", enableUnityLogs) != enableUnityLogs)
            {
                enableUnityLogs = !enableUnityLogs;
                if (enableUnityLogs)
                {
                    Messages.EnableUnityLogs();
                }
                else
                {
                    Messages.DisableUnityLogs();
                }
            }

            EditorGUILayout.Separator();
            EditorGUILayout.Separator();
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            if (stopped)
            {
                if (GUILayout.Button("Start", GUILayout.Width(70)))
                {
                    Messages.Log("Starting TheiaVR plugin");
                    try
                    {
                        StreamController.GetInstance().Start(enableSkeleton, enablePointCloud);
                        DisplayStopUI();
                    }
                    catch (Exception vException)
                    {
                        Messages.Log("<color=red>" + vException.Message + "</color>");
                    }

                    Messages.Log("TheiaVR correctly started");
                }
            }

            if (started)
            {
                if (GUILayout.Button("Stop", GUILayout.Width(70)))
                {
                    Messages.Log("Stopping TheiaVR plugin");
                    try
                    {
                        StreamController.GetInstance().Stop();
                        DisplayStartUI();
                    }
                    catch (Exception vException)
                    {
                        Messages.Log("<color=red>" + vException.Message + "</color>");
                    }
                }
            }

            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
        }
    }
}