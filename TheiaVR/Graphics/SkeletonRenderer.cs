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

        private new void Start()
        {
            base.Start();
            instance = this;

            GameObject vGameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            
            Vector3 vScale = new Vector3(0.1f, 0.1f, 0.1f);
            vGameObject.transform.localScale = vScale;

            base.SetGameObject(vGameObject);
        }
    }
}
    