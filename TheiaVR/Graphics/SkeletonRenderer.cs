using UnityEngine;

namespace TheiaVR.Graphics
{
    public class SkeletonRenderer : KinectRenderer
    {
        private static SkeletonRenderer instance;
        
        public static SkeletonRenderer GetInstance()
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
    