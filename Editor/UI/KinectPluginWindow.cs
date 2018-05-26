using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework.Constraints;
using TheiaVR.Controllers;
using TheiaVR.Helpers;
using UnityEngine;
using UnityEditor;

namespace TheiaVR.Editor
{
    public class KinectPluginWindow : EditorWindow
    {
        string ip;
        string cloudPort;
        string skelPort;
        bool enablePointCloud;
        bool enableSkeleton;
        private string logs = "";
        private Vector2 scroll;

        // Add menu item named "Kinect Plugin" to the Window menu
        [MenuItem("Window/Kinect Plugin")]
        public static void ShowWindow()
        {
            EditorWindow.GetWindow(typeof(KinectPluginWindow));
        }

        private void OnEnable()
        {
            ip = EditorPrefs.GetString("ip","127.0.0.1");
            cloudPort = EditorPrefs.GetString("cloudPort","9876");
            skelPort = EditorPrefs.GetString("skelPort","9877");
            enablePointCloud = EditorPrefs.GetBool("enablePointCloud",true);
            enableSkeleton = EditorPrefs.GetBool("enableSkeleton",true);
            Debug.Log("Got data!!");
        }

        private void OnDisable()
        {
            EditorPrefs.SetString("ip", ip);
            EditorPrefs.SetString("cloudPort", cloudPort);
            EditorPrefs.SetString("skelPort", skelPort);
            EditorPrefs.SetBool("enablePointCloud", enablePointCloud);
            EditorPrefs.SetBool("enableSkeleton", enableSkeleton);
            Debug.Log("Saved data!!");
        }

        void OnGUI()
        {
            // The actual window code goes here
            GUILayout.Label("Network settings", EditorStyles.boldLabel);
            ip = EditorGUILayout.TextField("IP Address", ip);
            cloudPort = EditorGUILayout.TextField("Cloud port", cloudPort);
            skelPort = EditorGUILayout.TextField("Skeleton port", skelPort);
            GUILayout.Label("Receiving", EditorStyles.boldLabel);
            enablePointCloud = EditorGUILayout.Toggle("Cloud points", enablePointCloud);
            enableSkeleton = EditorGUILayout.Toggle("Skeleton", enableSkeleton);

            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Start", GUILayout.Width(70)))
            {
                Messages.Log("Starting TheiaVR plugin");
                try
                {
                    StreamController.GetInstance().Start(enableSkeleton, enablePointCloud);
                }catch(Exception vException)
                {
                    Messages.Log("<color=red>" + vException.Message + "</color>");
                }
                Messages.Log("TheiaVR correctly started");
            }

            if (GUILayout.Button("Stop", GUILayout.Width(70)))
            {
                Messages.Log("Stopping TheiaVR plugin");
                try
                {
                    StreamController.GetInstance().Stop();
                }
                catch (Exception vException)
                {
                    Messages.Log("<color=red>" + vException.Message + "</color>");
                }
            }

            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();

            GUILayout.Label("Logs", EditorStyles.boldLabel);
            if (logs != "")
            {
                scroll = EditorGUILayout.BeginScrollView(scroll);
                GUILayout.Label(logs, "textfield");
                EditorGUILayout.EndScrollView();
            }
            else
            {
                GUILayout.Label("No logs for now");
            }

            if (GUILayout.Button("Clear", GUILayout.Width(70)))
            {
                Debug.Log("Clicked the clear button");
                logs = "";
            }
        }

        void AddToLogs(string newLog)
        {
            logs += newLog;
            logs += "\n";
            // TODO : scroll to bottom
        }
    }
}