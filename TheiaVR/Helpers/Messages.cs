using System;
using UnityEngine;

namespace TheiaVR.Helpers
{
    class Messages
    {
        private static bool logs = true;
        private static bool unityLogs = false;

        public static void EnableLogs()
        {
            logs = true;
        }

        public static void DisableLogs()
        {
            logs = false;
        }

        public static void EnableUnityLogs()
        {
            unityLogs = true;
        }

        public static void DisableUnityLogs()
        {
            unityLogs = false;
        }

        public static void Log(string aMessage)
        {
            if (logs)
            {
                if (unityLogs)
                {
                    Debug.Log(aMessage);
                }
                Console.WriteLine(aMessage);
            }
        }

        public static void LogWarning(string aMessage)
        {
            if (logs)
            {
                if (unityLogs)
                {
                    Debug.Log("<color=yellow>" + aMessage + "</color>");
                }
                Console.WriteLine(aMessage);
            }
        }

        public static void LogError(string aMessage)
        {
            if (logs)
            {
                if (unityLogs)
                {
                    Debug.Log("<color=red>" + aMessage + "</color>");
                }
                Console.WriteLine(aMessage);
            }
        }
    }
}
