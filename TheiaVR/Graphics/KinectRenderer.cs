using TheiaVR.Model;
using System.Collections.Generic;
using TheiaVR.Controllers;
using TheiaVR.Helpers;
using UnityEngine;

namespace TheiaVR.Graphics
{
    [RequireComponent(typeof(MeshFilter))]
    public abstract class KinectRenderer : MonoBehaviour
    {
        protected Mesh mesh;

        protected FrameBuffer buffer;

        protected List<GameObject> gameObjects;

        protected void SetMesh(Mesh aMesh)
        {
            mesh = aMesh;
        }

        public void SetBuffer(FrameBuffer aBuffer){
            buffer = aBuffer;
        }
        
        protected void Update()
        {
            if (buffer != null && !buffer.IsEmpty())
            {
                Frame vFrame = buffer.Dequeue();

                mesh.Clear();
                mesh.vertices = vFrame.GetVectors();
                mesh.colors = vFrame.GetColors();
                mesh.SetIndices(vFrame.GetIndices(), MeshTopology.Points, 0);
            }
        }

        protected void OnApplicationQuit()
        {
            StreamController.GetInstance().Stop();
        }

    }
}
