using System.Collections.Generic;
using TheiaVR.Helpers;
using TheiaVR.Model;
using UnityEngine;

namespace TheiaVR.Graphics
{
    [RequireComponent(typeof(MeshFilter))]
    public class CloudRenderer : KinectRenderer
    {
        private Mesh mesh;

        private void Awake()
        {
            mesh = GetComponent<MeshFilter>().mesh;
        }

        private new void Update()
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

    }
}