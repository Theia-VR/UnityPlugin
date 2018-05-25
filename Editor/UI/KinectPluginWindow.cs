using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEditor;

namespace TheiaVR.Editor
{
    public class KinectPluginWindow : EditorWindow
    {
        // TODO : get the ip from persisted file
        string ip = "127.0.0.1";
        string cloudPort = "9876";
        string skelPort = "9877";
        bool enablePointCloud = true;
        bool enableSkeleton = true;
        private string logs;
        private Vector2 scroll;

        // Add menu item named "Kinect Plugin" to the Window menu
        [MenuItem ("Window/Kinect Plugin")]
        public static void ShowWindow ()
        {
            EditorWindow.GetWindow(typeof(KinectPluginWindow));       
        }

        void OnGUI ()
        {
            // The actual window code goes here
            GUILayout.Label("Base Settings", EditorStyles.boldLabel);
            ip = EditorGUILayout.TextField("Adresse IP du Streamer", ip);
            cloudPort = EditorGUILayout.TextField("Port nuage", cloudPort);
            skelPort = EditorGUILayout.TextField("Port squel.", skelPort);
            enablePointCloud = EditorGUILayout.Toggle("Envoi du nuage de points", enablePointCloud);
            enableSkeleton = EditorGUILayout.Toggle("Envoi du squelette", enableSkeleton);

            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if(GUILayout.Button("Start",GUILayout.Width(70)))
            {
                Debug.Log("Clicked the start button");
                AddToLogs("Clicked the start button");
            }
            
            if(GUILayout.Button("Stop",GUILayout.Width(70)))
            {
                Debug.Log("Clicked the stop button");
                AddToLogs("Clicked the stop button");
            }
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
            
            GUILayout.Label("Logs", EditorStyles.boldLabel);
            scroll = EditorGUILayout.BeginScrollView(scroll);
            logs = EditorGUILayout.TextArea(logs);
            EditorGUILayout.EndScrollView();    
            if(GUILayout.Button("Clear",GUILayout.Width(70)))
            {
                Debug.Log("Clicked the clear button");
                logs = "";
            }
        }

        void AddToLogs(string newLog)
        {
            logs += newLog;
            logs += "\n";
//            scroll.y = ;
        }
        
        
    }
}
