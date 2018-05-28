using System;
using TheiaVR.Controllers;
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
        [MenuItem("Window/Kinect Plugin")]
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
                started = true;
                stopped = false;
            }
            else
            {
                started = false;
                stopped = true;
            }
        }
        
        void OnGUI()
        {
            GUILayout.Label("Network settings", EditorStyles.boldLabel);
            ip = EditorGUILayout.TextField("IP Address", ip);

            cloudPort = EditorGUILayout.IntField("Cloud port", cloudPort);
            skelPort = EditorGUILayout.IntField("Skeleton port", skelPort);

            GUILayout.Label("Receiving", EditorStyles.boldLabel);
            enablePointCloud = EditorGUILayout.Toggle("Cloud points", enablePointCloud);
            enableSkeleton = EditorGUILayout.Toggle("Skeleton", enableSkeleton);

            GUILayout.Label("Logs", EditorStyles.boldLabel);
            enableUnityLogs = EditorGUILayout.Toggle("Display Unity logs", enableUnityLogs);
            if (enableUnityLogs)
            {
                Messages.EnableUnityLogs();
            }
            else
            {
                Messages.DisableUnityLogs();
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
                        stopped = false;
                        started = true;
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
                        stopped = true;
                        started = false;
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