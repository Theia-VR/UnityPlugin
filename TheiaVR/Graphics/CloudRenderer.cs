using System.Collections.Generic;
using TheiaVR.Helpers;
using TheiaVR.Model;
using UnityEngine;

namespace TheiaVR.Graphics
{
    public class CloudRenderer : KinectRenderer
    {
        private static CloudRenderer instance;
        
        public static CloudRenderer GetInstance()
        {
            return instance;
        }

        private void Awake()
        {
            base.SetMesh(GetComponent<MeshFilter>().mesh);
        }

        private void Start()
        {
            instance = this;
        }

    }
}