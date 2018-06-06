using TheiaVR.Model;
using System.Collections.Generic;
using TheiaVR.Controllers;
using TheiaVR.Helpers;
using UnityEngine;

namespace TheiaVR.Graphics
{
    public abstract class KinectRenderer : MonoBehaviour
    {
        protected FrameBuffer buffer;
        
        public void SetBuffer(FrameBuffer aBuffer){
            buffer = aBuffer;
        }
        
        protected void OnApplicationQuit()
        {
            PluginController.GetInstance().Stop();
        }

    }
}
