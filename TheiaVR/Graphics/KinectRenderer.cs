using TheiaVR.Model;
using System.Collections.Generic;
using TheiaVR.Controllers;
using UnityEngine;

namespace TheiaVR.Graphics
{
    public abstract class KinectRenderer : MonoBehaviour
    {
        protected Frame frame;
        
        public void SetFrame(Frame aFrame){
            frame = aFrame;
        }
        
        protected void OnApplicationQuit()
        {
            PluginController.GetInstance().Stop();
        }

    }
}
